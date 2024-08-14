using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Web;

public partial class Admins : ComponentBase
{
    #region [ Fields ]

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
    public int CurrentPage { get; set; } = 1;
    public int PaginationCount { get; set; } = 0;

    public string? SearchText = string.Empty;
    public ICollection<User>? UsersInDatabase { get; set; }
    public IQueryable<AdminWithPayment>? Users { get; set; }
    #endregion

    #region [ Overrides ]

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await RefreshAsync();
    }
    #endregion

    #region [ Methods ]

    private async Task RefreshAsync(int index = 1)
    {
        Users = null;
        var itemsPerPage = 15;

        var usersIdWithAdminRole = await identityDbContext.UserRoles
                                                        .Where(x => x.RoleId == MasterDataConstants.AdminRoleId)
                                                        .Select(x => x.UserId)
                                                        .ToListAsync();

        UsersInDatabase = await usersRepository.FindAll(x => x.IsDeleted == false
                                                            && usersIdWithAdminRole.Contains(x.Id))
                                              .ToListAsync();
        PaginationCount = (int)Math.Ceiling((double)UsersInDatabase.Count / itemsPerPage);
        var billResult = await billsRepository.FindAll(x => !x.IsDeleted)
                                              .ToListAsync();
        var usersData = UsersInDatabase
            .GroupJoin(
                billResult,
                user => user.Id,
                bill => bill.UserId,
                (user, userBills) => new AdminWithPayment
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

        Users = usersData
        .Skip((index - 1) * itemsPerPage)
        .Take(itemsPerPage)
        .AsQueryable();

        StateHasChanged();
    }

    private async Task AddAdminDialog()
    {
        AdminWithAvatarFile user = new()
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
        };
        await DialogService.ShowDialogAsync<AddAdminDialog>(user, new DialogParameters()
        {
            Title = $"Create new member",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result)),
            Width = "700px",
            Height = "565px",
            TrapFocus = true,
            Modal = true,
            PrimaryActionEnabled = false
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

        AdminWithAvatarFile? user = result.Data as AdminWithAvatarFile;

        if (user is null ||
            user.Email is null ||
            user.PhoneNumber is null ||
            user.FirstName is null ||
            user.UserName is null ||
            !user.IsEmailValid ||
            !user.IsMiddleNameValid ||
            !user.IsFirstNameValid ||
            !user.IsLastNameValid ||
            !user.IsPhoneNumberValid)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough required information");
            return;
        }

        UserSignUpDTO dto = new(
            user.UserName,
            user.FormPassword is null ? "Welkom112!!@" : user.FormPassword,
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

        var newAdminInfo = await identityDbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
        if (newAdminInfo is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Something wrong please contact the devs and admins");
            return;
        }

        await identityDbContext.UserRoles.AddAsync(new UserRole()
        {
            RoleId = MasterDataConstants.AdminRoleId,
            UserId = newAdminInfo.Id
        });

        await identityDbContext.SaveChangesAsync();

        //Register Admin role


        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, $"User {user.UserName} added !!!");
    }
    private async Task Delete(AdminWithPayment userInfo)
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


    private async Task ResetToDefaultPasswordAsync(AdminWithPayment userInfo)
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

    private PresenceStatus StatusConverter(bool isFullyPaid)
        => isFullyPaid ? PresenceStatus.Available : PresenceStatus.Busy;

    private string StatusTextConverter(bool isFullyPaid)
        => isFullyPaid ? "Fully Paid" : "Not Fully Paid";
    private Appearance PageButtonAppearance(int pageIndex)
        => CurrentPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    #endregion
}
