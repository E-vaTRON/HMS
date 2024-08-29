using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using Blazored.LocalStorage;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Web;

public partial class Members : AuthenticationComponentBase
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
    private IUsersRepository usersRepository { get; set; } = null!;

    [Inject]
    private IServicesRepository servicesRepository { get; set; } = null!;

    [Inject]
    private IBillsRepository billsRepository { get; set; } = null!;

    [Inject]
    private IAuthenticationService authenticationService { get; set; } = null!;

    [Inject]
    private IdentityDbContext identityDbContext { get; set; } = null!;
    #endregion

    #region [ Properties ]
    User? userInfo;
    public int CurrentPage { get; set; } = 1;
    public int PaginationCount { get; set; } = 0;
    public string BlockRadioChoice { get; set; } = default!;
    public string PhaseRadioChoice { get; set; } = default!;
    public string LastNameFilter { get; set; } = string.Empty;
    public string StreetFilter { get; set; } = string.Empty;
    public ICollection<User>? UsersInDatabase { get; set; }
    public IQueryable<MemberWithPayment>? Users { get; set; }
    public IQueryable<MemberWithPayment>? UsersPaidInfo { get; set; }

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


            userInfo = await usersRepository.FindByGuidAsync(userGuid);
            if (userInfo is null)
                return;

            await RefreshAsync();
        }
    }
    #endregion

    #region [ Methods ]

    private async Task RefreshAsync(int index = 1,
                                    string blockSorting = "1",
                                    string phaseSorting = "1",
                                    string lastNameFilter = "",
                                    string streetFilter = "")
    {
        Users = null;
        var itemsPerPage = 15;
        var usersIdWithAdminRole = await identityDbContext.UserRoles
                                                        .Where(x => x.RoleId == MasterDataConstants.AdminRoleId)
                                                        .Select(x => x.UserId)
                                                        .ToListAsync();

        UsersInDatabase = await usersRepository.FindAll(x => x.IsDeleted == false && !usersIdWithAdminRole.Contains(x.Id))
                                               .ToListAsync();
        PaginationCount = (int)Math.Ceiling((double)UsersInDatabase.Count / itemsPerPage);
        var userResult = UsersInDatabase;
        var billResult = await billsRepository.FindAll(x => !x.IsDeleted)
                                              .ToListAsync();
        var usersData = userResult
            .GroupJoin(
                billResult,
                user => user.Id,
                bill => bill.UserId,
                (user, userBills) => new MemberWithPayment
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl,
                    Block = user.Block,
                    Lot = user.Lot,
                    LotSize = user.LotSize,
                    Phase = user.Phase,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Street = user.Street,
                    PhoneNumber = user.PhoneNumber,
                    CreatedAt = user.CreatedAt,
                    IsFullyPaid = !userBills.Any(x => x.PaidDate == null),
                    Bills = userBills.ToList()
                })
            .OrderByDescending(user => user.CreatedAt);


        if (!string.IsNullOrWhiteSpace(lastNameFilter))
        {
            string searchTerm = lastNameFilter.ToLower();

            if (searchTerm.Length > 0)
            {
                usersData = usersData.Where(x => x.LastName.ToLower().Contains(searchTerm))
                                     .OrderByDescending(user => user.CreatedAt);
            }
        }

        if (!string.IsNullOrWhiteSpace(streetFilter))
        {
            string searchTerm = streetFilter.ToLower();

            if (searchTerm.Length > 0)
            {
                usersData = usersData.Where(x => x.Street != null && x.Street.ToLower().Contains(searchTerm))
                                     .OrderByDescending(user => user.CreatedAt);
            }
        }

        if (blockSorting != "1")
        {
            if (blockSorting == "2")
            {
                usersData = usersData.OrderBy(x => x.Block);
            }
            else if (blockSorting == "3")
            {
                usersData = usersData.OrderByDescending(x => x.Block);
            }
        }
        else BlockRadioChoice = "1";

        if (phaseSorting != "1")
        {
            if (phaseSorting == "2")
            {
                usersData = usersData.OrderBy(x => x.Phase);
            }
            else if (phaseSorting == "3")
            {
                usersData = usersData.OrderByDescending(x => x.Phase);
            }
        }
        else PhaseRadioChoice = "1";

        Users = usersData
                .Skip((index - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .AsQueryable();

        UsersPaidInfo = UsersInDatabase.GroupJoin(billResult,
                                                   user => user.Id,
                                                   bill => bill.UserId,
                                                   (user, userBills) => new MemberWithPayment
                                                   {
                                                       IsFullyPaid = !userBills.Any(x => x.PaidDate == null)
                                                   }).AsQueryable();

        StateHasChanged();
    }
    private async Task SelectMemberByFirstNameAsync(string firstName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName) || Users is null)
            return;

        var userInfo = Users.FirstOrDefault(x => x.FirstName == firstName);
        if (userInfo is null)
            return;

        await OpenMemberPaymentRecord(userInfo);
    }
    private async Task OpenMemberPaymentRecord(MemberWithPayment userInfo)
    {
        foreach (var bill in userInfo.Bills)
        {
            var services = await servicesRepository.GetByBillId(bill.Id.ToString());
            bill.BillServices = new List<BillServices>(services.Select(x => new BillServices
            {
                BillId = bill.Id,
                ServiceId = x.Id,
                Service = x,
                Bill = null,
            }));
        }
        await DialogService.ShowDialogAsync<MemberPaymentRecord>(userInfo, new DialogParameters()
        {
            Title = $"{userInfo.FirstName} {userInfo.MiddleName} {userInfo.LastName}",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => { }),
            Width = "980px",
            Height = "565px",
            TrapFocus = true,
            Modal = true,
        });
    }
    private async Task AddMemberDialog()
    {
        MemberWithAvatarFile user = new()
        {
            FirstName = string.Empty,
            MiddleName = string.Empty,
            LastName = string.Empty,
            Email = string.Empty,
            PhoneNumber = string.Empty,
            UserName = string.Empty,
            Block = 0,
            Lot = 0,
            LotSize = 0,
            Phase = 0,
            CreatedAt = DateTime.Now
        };
        await DialogService.ShowDialogAsync<AddMemberDialog>(user, new DialogParameters()
        {
            Title = $"Create new member",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result)),
            Width = "700px",
            Height = "500px",
            TrapFocus = true,
            Modal = true,
            PrimaryActionEnabled = false
        });
    }
    private async Task EditMemberDialog(MemberWithPayment userInfo)
    {
        await DialogService.ShowDialogAsync<EditMemberDialog>(userInfo, new DialogParameters()
        {
            Title = $"Edit {userInfo.FirstName} {userInfo.LastName}",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleEditDialog(result)),
            Width = "700px",
            Height = "500px",
            TrapFocus = true,
            Modal = true,
        });
    }
    private async Task HandleDialog(DialogResult result)
    {
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough required information");
            return;
        }

        MemberWithAvatarFile? user = result.Data as MemberWithAvatarFile;

        if (user is null ||
            user.Email is null ||
            user.PhoneNumber is null ||
            user.FirstName is null ||
            user.UserName is null ||
            !user.IsMiddleNameValid)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough required information");
            return;
        }

        UserSignUpDTO dto = new(
            user.UserName,
            "Welkom112!!@",
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.RolesId,
            user.Phase,
            user.Lot,
            user.LotSize,
            user.Block,
            user.Street,
            user.AvatarFiles?.FirstOrDefault(),
            user.CreatedAt);
        await authenticationService.Register(dto, new());
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, $"User {user.UserName} added !!!");
    }
    private async Task HandleEditDialog(DialogResult result)
    {
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough required information");
            return;
        }

        MemberWithPayment? user = result.Data as MemberWithPayment;

        if (user is null ||
            user.Email is null ||
            user.PhoneNumber is null ||
            user.FirstName is null ||
            user.UserName is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough required information");
            return;
        }

        var userInfo = await usersRepository.FindByGuidAsync(user.Id);
        if (userInfo is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "User not found in database, maybe it was deleted already.");
            return;
        }

        userInfo.UserName = user.UserName;
        userInfo.FirstName = user.FirstName;
        userInfo.MiddleName = user.MiddleName;
        userInfo.LastName = user.LastName;
        userInfo.Email = user.Email;
        userInfo.PhoneNumber = user.PhoneNumber;
        userInfo.Phase = user.Phase;
        userInfo.Lot = user.Lot;
        userInfo.LotSize = user.LotSize;
        userInfo.Block = user.Block;
        userInfo.Street = user.Street;

        identityDbContext.Update(userInfo);
        await identityDbContext.SaveChangesAsync();

        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, $"User {user.UserName} added !!!");
    }
    private async Task Delete(MemberWithPayment userInfo)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {userInfo.Email} will be move to archive?",
                                                                "Yes", "No",
                                                                "Do you want to delete this user?");

        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await usersRepository.FindByGuidAsync(userInfo.Id);
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "User not found in database, maybe it was deleted already.");
            return;
        }

        entity.IsDeleted = true;
        await usersRepository.UpdateAsync(entity);
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, $"User {entity.UserName} moved to archive !!!");
    }

    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied to clipboard");
    }

    private async Task ResetToDefaultPasswordAsync(MemberWithPayment userInfo)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Password for {userInfo.Email} will be reset to default?",
                                                                "Yes", "No",
                                                                "Do you want to reset password?");

        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await usersRepository.FindByGuidAsync(userInfo.Id);
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "User not found in database, maybe it was deleted already.");
            return;
        }

        var resetToken = await usersRepository.GetResetPasswordTokenAsync(entity.Id);
        if (resetToken is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Something is wrong with the reset token.");
            return;
        }

        await usersRepository.ChangePasswordWithTokenAsync(entity.Id, resetToken, MasterDataConstants.DefaultPassword);
        ToastService.ShowToast(ToastIntent.Success, $"Password for {entity.UserName} reset to default !!!");
    }
    private Appearance PageButtonAppearance(int pageIndex)
        => CurrentPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    private PresenceStatus StatusConverter(bool isFullyPaid)
        => isFullyPaid ? PresenceStatus.Available : PresenceStatus.Busy;
    private string StatusTextConverter(bool isFullyPaid)
        => isFullyPaid ? "Fully Paid" : "Not Fully Paid";
    async Task HandleTextFilterInput()
        => await RefreshAsync(CurrentPage,
                         BlockRadioChoice,
                         PhaseRadioChoice,
                         LastNameFilter,
                         StreetFilter);
    void ClearFormats()
    {
        LastNameFilter = string.Empty;
        StreetFilter = string.Empty;
        BlockRadioChoice = "1";
        RefreshAsync(CurrentPage, "1", "1", LastNameFilter, StreetFilter);
    }
    void SelectedChoice()
    {
        RefreshAsync(CurrentPage,
                     BlockRadioChoice,
                     PhaseRadioChoice,
                     LastNameFilter,
                     StreetFilter);
    }

    private bool IsFinancist(User user)
    {
        return MasterDataConstants.FinancistIds.Contains(user.Id);
    }
    #endregion

    #region [ Excel ]

    async Task<byte[]> GenerateExcel(ICollection<User> users)
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
                Cell header1 = new Cell() { CellReference = "A1", CellValue = new CellValue("User Name"), DataType = CellValues.String };
                headerRow.Append(header1);
                Cell header2 = new Cell() { CellReference = "B1", CellValue = new CellValue("First Name"), DataType = CellValues.String };
                headerRow.Append(header2);
                Cell header3 = new Cell() { CellReference = "C1", CellValue = new CellValue("Last Name"), DataType = CellValues.String };
                headerRow.Append(header3);
                Cell header4 = new Cell() { CellReference = "D1", CellValue = new CellValue("Phase"), DataType = CellValues.String };
                headerRow.Append(header4);
                Cell header5 = new Cell() { CellReference = "E1", CellValue = new CellValue("Block"), DataType = CellValues.String };
                headerRow.Append(header5);
                Cell header6 = new Cell() { CellReference = "F1", CellValue = new CellValue("Street"), DataType = CellValues.String };
                headerRow.Append(header6);
                Cell header7 = new Cell() { CellReference = "G1", CellValue = new CellValue("Email"), DataType = CellValues.String };
                headerRow.Append(header7);

                // Add user data
                foreach (var user in users)
                {
                    Row dataRow = new Row();
                    dataRow.Append(CreateTextCell(user.UserName ?? string.Empty));
                    dataRow.Append(CreateTextCell(user.FirstName));
                    dataRow.Append(CreateTextCell(user.LastName ?? string.Empty));
                    dataRow.Append(CreateNumberCell(user.Phase.ToString()));
                    dataRow.Append(CreateNumberCell(user.Block.ToString()));
                    dataRow.Append(CreateTextCell(user.Street ?? string.Empty));
                    dataRow.Append(CreateTextCell(user.Email));
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
        if (UsersInDatabase is null)
            return;

        string fileName = "HMS members";
        byte[] excelData = await GenerateExcel(UsersInDatabase);
        await JSRuntime.InvokeVoidAsync("saveAsFile",
                                        fileName,
                                        Convert.ToBase64String(excelData),
                                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    async Task UploadExcel()
    {
        var data = new UploadMembersExcelFileModel();
        await DialogService.ShowDialogAsync<UploadMembersExcelDialog>(data, new DialogParameters()
        {
            Title = $"Import excel",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleExcelDialog(result)),
            Width = "500px",
            Height = "500px",
            TrapFocus = true,
            Modal = true,
        });
    }

    private async Task HandleExcelDialog(DialogResult result)
    {
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create an drug");
            return;
        }

        var file = result.Data as UploadMembersExcelFileModel;
        if (file == null)
        {
            return;
        }
        await usersRepository.AddRange(file.Users);
        await this.RefreshAsync();
    }
    #endregion
}
