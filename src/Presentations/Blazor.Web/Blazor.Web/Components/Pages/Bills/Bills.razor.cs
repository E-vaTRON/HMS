using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
namespace Blazor.Web;

public partial class Bills : AuthenticationComponentBase
{
    #region [ Fields ]

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    private IToastService ToastService { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private IBillsService billsService { get; set; } = null!;

    [Inject]
    private IUsersRepository usersRepository { get; set; } = null!;

    [Inject]
    private IBillsRepository billsRepository { get; set; } = null!;

    [Inject]
    private IServicesRepository servicesRepository { get; set; } = null!;

    [Inject]
    private IDrugRepository drugRepository { get; set; } = null!;

    [Inject]
    private IBillServicesRepository billServicesRepository { get; set; } = null!;

    [Inject]
    private IRequestRepository requestRepository { get; set; } = null!;

    [Inject]
    private UserManager userManager { get; set; } = null!;

    [Inject]
    private ApplicationDbContext context { get; set; } = null!;

    [Inject]
    private IOptionsMonitor<List<FloorModel>> floorsConfig { get; set; } = null!;

    [Inject]
    private IOptionsMonitor<List<SymptomGroupModel>> symptomsGroupConfig { get; set; } = null!;
    #endregion

    #region [ Properties ]
    User? userInfo;
    public int CurrentPage { get; set; } = 1;
    public int PaginationCount { get; set; } = 0;
    public string LastNameFilter = string.Empty;
    public string AmountRadioChoice { get; set; } = default!;
    public string CreatedDateRadioChoice { get; set; } = default!;
    public string DeadlineRadioChoice { get; set; } = default!;
    public IEnumerable<BillWithServicesDTO>? BillsInDatabases { get; set; }
    public IQueryable<BillWithMemberAndServicesInfo>? Items { get; set; }
    public List<FloorModel> Floors { get; set; } = new List<FloorModel>();
    public List<SymptomGroupModel> SymptomGroups { get; set; } = new List<SymptomGroupModel>();
    #endregion

    #region [ Overrides ]

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {

            if (await base.IsNotLogin())
            {
                this.NavigationManager.NavigateTo("/login", replace: true);
                return;
            }

            var userGuid = await LocalStorage.GetItemAsync<string>("userId");
            if (userGuid is null)
                return;


            userInfo = await userManager.FindByGuidAsync(userGuid);
            if (userInfo is null)
                return;

            await RefreshAsync();
        }
    }
    #endregion

    #region [ Methods ]

    private async Task RefreshAsync(int index = 1,
                                    string lastNameFilter = "",
                                    string amountSorting = "1",
                                    string createdDateSorting = "1",
                                    string deadlineSorting = "1")
    {
        Items = null;
        var itemsPerPage = 15;
        SymptomGroups = symptomsGroupConfig.CurrentValue;
        Floors = floorsConfig.CurrentValue;
        var roomEvents = await context.RoomEvents.ToListAsync();
        if (roomEvents.Any())
        {
            // Iterate through each floor
            foreach (var floor in Floors)
            {
                // Iterate through each room in the floor
                foreach (var room in floor.Rooms)
                {
                    // Find the events that belong to the current room
                    var eventsForRoom = roomEvents
                        .Where(e => e.RoomId == room.Id)
                        .Select(e => new RoomEvent
                        {
                            Id = e.Id,
                            RoomId = e.RoomId,
                            BillId = e.BillId,
                            ServiceId = e.ServiceId,
                            StartDate = e.StartDate,
                            EndDate = e.EndDate
                        })
                        .ToList();

                    // Assign the list of events to the room's Events property
                    room.Events = eventsForRoom;
                }
            }
        }

        var userResult = await usersRepository.FindAll().ToListAsync();
        var drugs = await drugRepository.FindAll().ToListAsync();
        var request = await requestRepository.FindAll(x => !x.IsDeleted).ToListAsync();
        BillsInDatabases = await billsService.GetBillWithServicesAsync(isDeleted: false);
        PaginationCount = (int)Math.Ceiling((double)BillsInDatabases
                                    .Where(x => x.PaidDate is null)
                                    .ToList().Count / itemsPerPage);
        var userLookup = userResult.ToDictionary(user => user.Id);
        var billsData = BillsInDatabases
            .Where(x => x.PaidDate is null)
            .Select(x => new BillWithMemberAndServicesInfo
            {
                Id = new Guid(x.Id),
                UserId = x.UserId,
                Deadline = x.Deadline,
                PaidDate = x.PaidDate,
                ExcessAmount = x.ExcessAmount,
                UnderPaidAmount = x.UnderPaidAmount,
                CreatedDate = x.CreatedDate,
                User = userLookup.ContainsKey(x.UserId) ? userLookup[x.UserId]! : null,
                Rooms = roomEvents
                .Where(y => y.BillId == x.Id)
                .Select(y => new BillRoom
                {
                    Name = y.RoomId
                }).ToList(),
                Services = x.Services.Select(x => new ServiceWithQuantity
                {
                    Id = new Guid(x.id!),
                    Name = x.name,
                    Color = x.color,
                    Price = x.price,
                    CalculationType = (CalculationType)x.calculationType,
                    Quantity = x.quantity
                }).ToList(),
                DrugInBills = string.IsNullOrWhiteSpace(x.DrugIdAndQuantity) ? [] : x.DrugIdAndQuantity.Trim('{', '}').Split("}{")
                .Select(idAndQuantity =>
                {
                    var pair = idAndQuantity.Split(",");
                    var id = pair[0];
                    var quantity = pair[1];
                    var drug = drugs.FirstOrDefault(x => x.Id.ToString() == id);
                    return new DrugInBill
                    {
                        Description = drug?.Description,
                        Quantity = int.Parse(quantity)
                    };
                }).ToList(),
                Request = request.FirstOrDefault(y => y.UserId == x.UserId),
            }).OrderByDescending(x => x.CreatedDate);

        if (!string.IsNullOrEmpty(lastNameFilter))
        {
            billsData = billsData.Where(x => x.User!.LastName!.ToLower().Contains(lastNameFilter.ToLower()))
                                 .OrderByDescending(x => x.CreatedDate);
        }

        if (amountSorting != "1")
        {
            if (amountSorting == "2")
            {
                billsData = billsData.OrderBy(x => x.Services.Sum(y => CalculateAmount(y, x.User)));
            }
            else if (amountSorting == "3")
            {
                billsData = billsData.OrderByDescending(x => x.Services.Sum(y => CalculateAmount(y, x.User)));
            }
        }
        else AmountRadioChoice = "1";

        if (createdDateSorting != "1")
        {
            if (createdDateSorting == "2")
            {
                billsData = billsData.OrderBy(x => x.CreatedDate);
            }
            else if (createdDateSorting == "3")
            {
                billsData = billsData.OrderByDescending(x => x.CreatedDate);
            }
        }
        else CreatedDateRadioChoice = "1";

        if (deadlineSorting != "1")
        {
            if (deadlineSorting == "2")
            {
                billsData = billsData.OrderBy(x => x.Deadline);
            }
            else if (deadlineSorting == "3")
            {
                billsData = billsData.OrderByDescending(x => x.Deadline);
            }
        }
        else DeadlineRadioChoice = "1";

        Items = billsData.Skip((index - 1) * itemsPerPage)
                         .Take(itemsPerPage)
                         .AsQueryable();

        StateHasChanged();
    }

    private async Task OpenBillDialog()
    {
        var services = servicesRepository.FindAll(x => !x.IsDeleted).ToList();
        var users = usersRepository.FindAll(x => !x.IsDeleted).ToList();
        var request = requestRepository.FindAll(x => !x.IsDeleted).ToList();

        // Filter out Doctors and Nurses
        var doctorAndNurseIds = MasterDataConstants.DoctorIds.Concat(MasterDataConstants.NurseIds).ToList();
        var filteredUsers = users.Where(user => doctorAndNurseIds.Contains(user.Id)).ToList();

        AddBillDialogParameter parameter = new()
        {
            ServicesData = services,
            Users = filteredUsers,
            Rooms = new List<RoomModel>(Floors.SelectMany(floor => floor.Rooms).ToList()),
            Symptoms = new List<SymptomModel>(SymptomGroups.SelectMany(symptom => symptom.Symptoms).ToList())
        };

        await DialogService.ShowDialogAsync<AddBillDialog>(parameter, new DialogParameters()
        {
            Title = $"Create new bill",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleAddDialog(result)),
            Width = "1000px",
            Height = "600px",
            TrapFocus = true,
            Modal = true,
            PrimaryActionEnabled = false
        });
    }

    private async Task EditBillDialog(BillWithMemberAndServicesInfo item)
    {
        var drugs = drugRepository.FindAll(x => !x.IsDeleted).ToList();
        var request = requestRepository.FindAll(x => !x.IsDeleted).ToList();

        EditBillDialogParameter parameter = new()
        {
            Id = item.Id,
            UserId = item.UserId,
            User = item.User,
            RequestId = item.RequestId,
            Request = item.Request,
            DrugIdAndQuantity = item.DrugIdAndQuantity,
            DrugInBills = item.DrugInBills,
            Drugs = drugs,
            Symptoms = new List<SymptomModel>(SymptomGroups.SelectMany(symptom => symptom.Symptoms).ToList())
        };

        await DialogService.ShowDialogAsync<EditBillDialog>(parameter, new DialogParameters()
        {
            Title = $"Add drugs to bill",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleEditDialog(result)),
            Width = "800px",
            Height = "600px",
            TrapFocus = true,
            Modal = true,
            PrimaryActionEnabled = false
        });
    }

    private async Task ProcessBillPayment(BillWithMemberAndServicesInfo item)
    {

        ProcessPaymentDialogParameter parameter = new()
        {
            Id = item.Id,
            Deadline = item.Deadline,
            User = item.User,
            Services = item.Services,
            PaidDate = DateTime.Now
        };
        await DialogService.ShowDialogAsync<ProcessPaymentDialog>(parameter, new DialogParameters()
        {
            Title = $"{item.User!.LastName}",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleProcessPaymentDialog(result)),
            Width = "450px",
            Height = "520px",
            TrapFocus = true,
            Modal = true,
        });
    }

    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied to clipboard");
    }

    private async Task HandleAddDialog(DialogResult result)
    {
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create a bill");
            return;
        }

        await context.Database.BeginTransactionAsync();

        AddBillDialogParameter? serviceInfo = result.Data as AddBillDialogParameter;
        if (serviceInfo is null || serviceInfo!.Deadline is null || !serviceInfo.SelectedServices.Any() || serviceInfo.SelectedUser is null || serviceInfo.SelectedRequest is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create a bill");
            return;
        }

        Bill bill = new()
        {
            UserId = serviceInfo.SelectedUser.Id,
            Deadline = serviceInfo.Deadline!.Value,
            PaidDate = null,
            ExcessAmount = default!,
            UnderPaidAmount = default!,
            CreatedDate = DateTime.Now,
            RequestId = serviceInfo.SelectedRequest.Id.ToString()
        };
        billsRepository.Add(bill);
        foreach (var service in serviceInfo.SelectedServices)
        {
            BillServices billServices = new()
            {
                Bill = bill,
                Service = service,
                CreatedDate = DateTime.Now,
                Quantity = serviceInfo.SelectedServicesWithQuantity.FirstOrDefault(x => x.Id == service.Id)!.Quantity,
            };
            billServicesRepository.Add(billServices);
        }

        if (serviceInfo.SelectedServicesWithQuantity.Any())
        {
            foreach (var item in serviceInfo.SelectedServicesWithQuantity)
            {
                if (item.SelectedRoom is not null)
                {
                    context.RoomEvents.Add(new()
                    {
                        UserIds = string.Join(",", item.SelectedUsers.Select(x => x.Id).ToList()),
                        BillId = bill.Id.ToString(),
                        RoomId = item.SelectedRoom.RoomId,
                        ServiceId = item.Id.ToString(),
                        //StartDate = CombineFullDateTime(item.SelectedRoom.StartDate, item.SelectedRoom.StartTime),
                        //EndDate = CombineFullDateTime(item.SelectedRoom.EndDate, item.SelectedRoom.EndTime)
                        StartDate = item.SelectedRoom.StartDate,
                        EndDate = item.SelectedRoom.EndDate
                    });
                }
            }
        }

        await billsRepository.SaveChangesAsync();
        await billServicesRepository.SaveChangesAsync();

        await context.Database.CommitTransactionAsync();

        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "New bill created !!!");
    }

    private async Task HandleEditDialog(DialogResult result)
    {
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create a bill");
            return;
        }

        //await context.Database.BeginTransactionAsync();

        EditBillDialogParameter? serviceInfo = result.Data as EditBillDialogParameter;
        if (serviceInfo is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create a bill");
            return;
        }
        var drugIdandQuantity = string.Empty;
        foreach (var drug in serviceInfo.SelectedDrugs)
        {
            drugIdandQuantity += $"{{{drug.Id},{drug.Quantity}}}";
        }

        var bill = await billsRepository.FindByIdAsync(serviceInfo.Id.ToString());
        if (bill is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "User not found in database, maybe it was deleted already.");
            return;
        }

        bill.DrugIdAndQuantity = drugIdandQuantity;

        billsRepository.Update(bill);
        await billsRepository.SaveChangesAsync();

        //await context.Database.CommitTransactionAsync();

        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "Added Drug to Bill !!!");
    }

    private DateTime? CombineFullDateTime(DateTime? date, DateTime? time)
    {

        if (date.HasValue && time.HasValue)
        {
            return date.Value.Date + time.Value.TimeOfDay;
        }
        return null;
    }

    private async Task HandleProcessPaymentDialog(DialogResult result)
    {
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create a bill");
            return;
        }

        ProcessPaymentDialogParameter? billInfo = result.Data as ProcessPaymentDialogParameter;
        if (billInfo is null || billInfo!.PaidDate is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create a payment");
            return;
        }

        var bill = await billsRepository.FindByIdAsync(billInfo.Id.ToString());
        if (bill is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Bill not found in database, maybe it was deleted already.");
            return;
        }

        bill.PaidDate = billInfo.PaidDate;
        bill.ExcessAmount = billInfo.ExcessAmount;
        bill.UnderPaidAmount = billInfo.UnderPaidAmount;
        billsRepository.Update(bill);
        await billsRepository.SaveChangesAsync();

        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "Payment processed !!!");
    }

    private async Task Delete(BillWithMemberAndServicesInfo bill)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {bill.Id} will be move to archive?",
                                                                "Yes", "No",
                                                                "Do you want to delete this record?");
        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await billsRepository.FindByIdAsync(bill.Id.ToString());
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Record not found in database, maybe it was deleted already.");
            return;
        }

        entity.IsDeleted = true;
        billsRepository.Update(entity);
        await billsRepository.SaveChangesAsync();
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "Record deleted successfully.");
    }

    private string Sum(BillWithMemberAndServicesInfo item)
    {
        decimal amount = default(decimal)!;

        foreach (var service in item.Services)
        {
            amount += CalculateAmount(service, item.User);
        }
        return amount.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
    }

    private decimal CalculateAmount(ServiceWithQuantity service, User? user)
    {
        decimal amount = default(decimal);

        if (user is null)
        {
            amount += (service.Price * service.Quantity);
        }
        else
        {
            switch (service.CalculationType)
            {
                case CalculationType.DirectAddition:
                    amount += (service.Price * service.Quantity);
                    break;
                case CalculationType.LotSizeMultiplication:
                    amount += service.Price * user.LotSize;
                    break;
                default:
                    amount += (service.Price * service.Quantity);
                    break;
            }
        }

        return amount;
    }

    private Appearance PageButtonAppearance(int pageIndex)
        => CurrentPage == pageIndex ? Appearance.Accent : Appearance.Neutral;

    async Task HandleTextFilterInput()
        => await RefreshAsync(CurrentPage,
                              LastNameFilter,
                              AmountRadioChoice,
                              CreatedDateRadioChoice,
                              DeadlineRadioChoice);

    async Task SelectedChoice()
        => await RefreshAsync(CurrentPage,
                                LastNameFilter,
                                AmountRadioChoice,
                                CreatedDateRadioChoice,
                                DeadlineRadioChoice);

    async Task ClearFormats()
    {
        LastNameFilter = string.Empty;
        AmountRadioChoice = "1";
        CreatedDateRadioChoice = "1";
        DeadlineRadioChoice = "1";
        await RefreshAsync(CurrentPage, LastNameFilter, "1", "1", "1");
    }

    private bool IsFinancist(User user)
    {
        return MasterDataConstants.FinancistIds.Contains(user.Id);
    }

    private bool IsDoctor(User user)
    {
        return MasterDataConstants.DoctorIds.Contains(user.Id);
    }
    #endregion

    #region [ Excel ]

    async Task<byte[]> GenerateExcel(ICollection<BillWithMemberAndServicesInfo> items)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Sheet1"  // You can customize the sheet name here
                };

                Row headerRow = new Row() { RowIndex = 1 };
                sheetData.Append(headerRow);

                // Add header cells
                Cell header1 = new Cell() { CellReference = "A1", CellValue = new CellValue("Services"), DataType = CellValues.String };
                headerRow.Append(header1);
                Cell header2 = new Cell() { CellReference = "B1", CellValue = new CellValue("Created Date"), DataType = CellValues.String };
                headerRow.Append(header2);
                Cell header3 = new Cell() { CellReference = "C1", CellValue = new CellValue("Deadline"), DataType = CellValues.String };
                headerRow.Append(header3);
                Cell header4 = new Cell() { CellReference = "D1", CellValue = new CellValue("Full name"), DataType = CellValues.String };
                headerRow.Append(header4);
                Cell header5 = new Cell() { CellReference = "E1", CellValue = new CellValue("Amount"), DataType = CellValues.String };
                headerRow.Append(header5);

                // Add user data
                foreach (var item in items)
                {
                    Row dataRow = new Row();
                    dataRow.Append(CreateTextCell(string.Join(", ", item.Services.Select(s => s.Name)) ?? string.Empty));
                    dataRow.Append(CreateTextCell(item.CreatedDate.ToString("dd/MM/yy")));
                    dataRow.Append(CreateTextCell(item.Deadline.ToString("dd/MM/yy")));
                    dataRow.Append(CreateTextCell(item.User!.FirstName + item.User.MiddleName ?? string.Empty + " " + item.User.LastName));
                    dataRow.Append(CreateTextCell(Sum(item)));
                    sheetData.AppendChild(dataRow);
                }

                // Append the sheet to the workbook
                sheets.Append(sheet);

                workbookPart.Workbook.Save();
            }

            return stream.ToArray();
        }
    }
    private Cell CreateTextCell(string text)
    {
        return new Cell(new CellValue(text))
        {
            DataType = new EnumValue<CellValues>(CellValues.String)
        };
    }
    private Cell CreateNumberCell(string number)
    {
        return new Cell(new CellValue(number))
        {
            DataType = new EnumValue<CellValues>(CellValues.Number)
        };
    }
    async Task DownloadExcel()
    {
        if (BillsInDatabases is null)
            return;

        string fileName = "HMS bills";

        var userResult = await usersRepository.FindAll().ToListAsync();
        var userLookup = userResult.ToDictionary(user => user.Id);
        var billsData = BillsInDatabases
            .Where(x => x.PaidDate is null)
            .Select(x => new BillWithMemberAndServicesInfo
            {
                Id = new Guid(x.Id),
                UserId = x.UserId,
                Deadline = x.Deadline,
                PaidDate = x.PaidDate,
                ExcessAmount = x.ExcessAmount,
                UnderPaidAmount = x.UnderPaidAmount,
                CreatedDate = x.CreatedDate,
                User = userLookup.ContainsKey(x.UserId) ? userLookup[x.UserId]! : null,
                Services = x.Services.Select(x => new ServiceWithQuantity
                {
                    Id = new Guid(x.id!),
                    Name = x.name,
                    Color = x.color,
                    Price = x.price,
                    CalculationType = (CalculationType)x.calculationType,
                    Quantity = x.quantity
                }).ToList()
            }).OrderByDescending(x => x.CreatedDate);

        byte[] excelData = await GenerateExcel(billsData.ToList());
        await JSRuntime.InvokeVoidAsync("saveAsFile",
                                        fileName,
                                        Convert.ToBase64String(excelData),
                                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }
    #endregion

}
