using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Blazor.Web;

public partial class Collections : AuthenticationComponentBase
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
    private IBillServicesRepository billServicesRepository { get; set; } = null!;

    [Inject]
    private ApplicationDbContext context { get; set; } = null!;
    #endregion

    #region [ Properties ]

    public string? SearchText = string.Empty;
    public decimal? Amount;
    public decimal Paid;
    public string? Percentage;

    public int? ProgressBarMaxValue;
    public int? ProgressBarValue;
    public int CurrentPage { get; set; } = 1;
    public int PaginationCount { get; set; } = 0;

    public string LastNameFilter = string.Empty;
    public string AmountRadioChoice { get; set; } = default!;
    public string CreatedDateRadioChoice { get; set; } = default!;
    public string DeadlineRadioChoice { get; set; } = default!;
    public IEnumerable<BillWithServicesDTO> BillsInDatabases { get; set; } = default!;
    public IQueryable<BillWithMemberAndServicesInfo>? Items { get; set; }
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

        var userResult = await usersRepository.FindAll().ToListAsync();
        BillsInDatabases = await billsService.GetBillWithServicesAsync();
        PaginationCount = (int)Math.Ceiling((double)BillsInDatabases.Where(x => x.PaidDate != null).ToList().Count / itemsPerPage);
        var userLookup = userResult.ToDictionary(user => user.Id);
        var items = BillsInDatabases.Select(x => new BillWithMemberAndServicesInfo
        {
            Id = new Guid(x.Id),
            UserId = x.UserId,
            Deadline = x.Deadline,
            PaidDate = x.PaidDate,
            CreatedDate = x.CreatedDate,
            ExcessAmount = x.ExcessAmount,
            UnderPaidAmount = x.UnderPaidAmount,
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

        if (!string.IsNullOrEmpty(lastNameFilter))
        {
            items = items.Where(x => x.User!
                                      .LastName!
                                      .ToLower()
                                      .Contains(lastNameFilter.ToLower()))
                                      .OrderByDescending(x => x.CreatedDate);
        }

        if (amountSorting != "1")
        {
            if (amountSorting == "2")
            {
                items = items.OrderBy(x => x.Services.Sum(y => CalculateAmount(y, x.User)));
            }
            else if (amountSorting == "2")
            {
                items = items.OrderByDescending(x => x.Services.Sum(y => CalculateAmount(y, x.User)));
            }
        }
        else AmountRadioChoice = "1";

        if (createdDateSorting != "1")
        {
            if (createdDateSorting == "2")
            {
                items = items.OrderBy(x => x.CreatedDate);
            }
            else if (createdDateSorting == "3")
            {
                items = items.OrderByDescending(x => x.CreatedDate);
            }
        }
        else CreatedDateRadioChoice = "1";

        if (deadlineSorting != "1")
        {
            if (deadlineSorting == "2")
            {
                items = items.OrderBy(x => x.Deadline);
            }
            else if (deadlineSorting == "3")
            {
                items = items.OrderByDescending(x => x.Deadline);
            }
        }
        else DeadlineRadioChoice = "1";

        Items = items.Where(x => x.PaidDate != null).Skip((index - 1) * itemsPerPage)
                     .Take(itemsPerPage)
                     .AsQueryable();

        Amount = items.Sum(x => this.Sum(x));
        if (Amount != 0)
        {
            Paid = items.Where(x => x.PaidDate != null).Sum(x => this.Sum(x));
            Percentage = ((Paid / Amount) * 100).Value.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
            ProgressBarMaxValue = Convert.ToInt32(Amount);
            ProgressBarValue = Convert.ToInt32(Paid);
        }

        StateHasChanged();
    }
    private async Task OpenBillDialog()
    {
        var services = servicesRepository.FindAll().ToList();
        AddBillDialogParameter parameter = new()
        {
            ServicesData = services
        };
        await DialogService.ShowDialogAsync<AddBillDialog>(parameter, new DialogParameters()
        {
            Title = $"Create new bill",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result)),
            Width = "300px",
            Height = "450px",
            TrapFocus = true,
            Modal = true,
        });
    }
    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied to clipboard");
    }
    private async Task HandleDialog(DialogResult result)
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
        if (serviceInfo is null || serviceInfo!.Deadline is null || !serviceInfo.SelectedServices.Any() || serviceInfo.SelectedUser is null)
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
        };
        billsRepository.Add(bill);
        foreach (var service in serviceInfo.SelectedServices)
        {
            BillServices billServices = new()
            {
                Bill = bill,
                Service = service
            };
            billServicesRepository.Add(billServices);
        }
        await billsRepository.SaveChangesAsync();
        await billServicesRepository.SaveChangesAsync();

        await context.Database.CommitTransactionAsync();

        await RefreshAsync();
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
    private decimal Sum(BillWithMemberAndServicesInfo item)
    {
        decimal amount = default(decimal)!;

        foreach (var service in item.Services)
        {
            amount += CalculateAmount(service, item.User);
        }
        return amount;
    }


    private decimal ActualPaid(BillWithMemberAndServicesInfo item)
    {
        decimal amount = default(decimal)!;

        foreach (var service in item.Services)
        {
            amount += CalculateAmount(service, item.User);
        }
        amount += item.ExcessAmount;
        amount -= item.UnderPaidAmount;
        return amount;
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
                //case CalculationType.LotSizeMultiplication:
                //    amount += service.Price * user.LotSize;
                //    break;
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
                Cell header4 = new Cell() { CellReference = "D1", CellValue = new CellValue("Paid Date"), DataType = CellValues.String };
                headerRow.Append(header4);
                Cell header5 = new Cell() { CellReference = "E1", CellValue = new CellValue("Full name"), DataType = CellValues.String };
                headerRow.Append(header5);
                Cell header6 = new Cell() { CellReference = "F1", CellValue = new CellValue("Amount"), DataType = CellValues.String };
                headerRow.Append(header6);
                Cell header7 = new Cell() { CellReference = "G1", CellValue = new CellValue("Excess"), DataType = CellValues.String };
                headerRow.Append(header7);
                Cell header8 = new Cell() { CellReference = "H1", CellValue = new CellValue("Under Paid"), DataType = CellValues.String };
                headerRow.Append(header8);

                // Add user data
                foreach (var item in items)
                {
                    Row dataRow = new Row();
                    dataRow.Append(CreateTextCell(string.Join(", ", item.Services.Select(s => s.Name)) ?? string.Empty));
                    dataRow.Append(CreateTextCell(item.CreatedDate.ToString("dd/MM/yy")));
                    dataRow.Append(CreateTextCell(item.Deadline.ToString("dd/MM/yy")));
                    dataRow.Append(CreateTextCell(item.PaidDate is not null ? item.PaidDate.Value.ToString("dd/MM/yy") : string.Empty));
                    dataRow.Append(CreateTextCell(item.User!.FirstName + item.User.MiddleName ?? string.Empty + " " + item.User.LastName));
                    dataRow.Append(CreateTextCell(Sum(item).ToString("C2", CultureInfo.GetCultureInfo("en-PH"))));
                    dataRow.Append(CreateTextCell(item.ExcessAmount.ToString("C2", CultureInfo.GetCultureInfo("en-PH"))));
                    dataRow.Append(CreateTextCell(item.UnderPaidAmount.ToString("C2", CultureInfo.GetCultureInfo("en-PH"))));
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

        string fileName = "HMS collections";

        var userResult = await usersRepository.FindAll().ToListAsync();
        var userLookup = userResult.ToDictionary(user => user.Id);
        var billsData = BillsInDatabases
            .Where(x => x.PaidDate is not null)
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
