using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Blazor.Web;

public partial class Archive : AuthenticationComponentBase
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

    public int CurrentUsersPage { get; set; } = 1;
    public int UsersPaginationCount { get; set; } = 0;


    public int CurrentBillsPage { get; set; } = 1;
    public int BillsPaginationCount { get; set; } = 0;

    public int CurrentServicesPage { get; set; } = 1;
    public int ServicesPaginationCount { get; set; } = 0;

    public string? SearchText = string.Empty;

    public ICollection<User> UsersInDatabase { get; set; }
    public IQueryable<User>? UserItems { get; set; }
    public IEnumerable<BillWithServicesDTO> BillsInDatabase { get; set; }
    public IQueryable<BillWithMemberAndServicesInfo>? BillItems { get; set; }
    public ICollection<Service>? ServicesInDatabase { get; set; }
    public IQueryable<ServiceModel>? ServiceItems { get; set; }
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
            //await RefreshAsync();
            await NavigateUsers();
            await NavigateBills();
            await NavigateServices();
        }
    }
    #endregion

    #region [ Methods ]

    private async Task RefreshAsync()
    {
        BillItems = null;
        UserItems = null;
        ServiceItems = null;

        var userResult = await usersRepository.FindAll().ToListAsync();
        var billWithServicesResult = await billsService.GetBillWithServicesAsync(isDeleted: true);
        var serviceResult = await servicesRepository.FindAll(x => x.IsDeleted).ToListAsync();

        var userLookup = userResult.ToDictionary(user => user.Id);
        BillItems = billWithServicesResult.Where(x => x.PaidDate is not null)
                                          .Select(x => new BillWithMemberAndServicesInfo
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
                                              }).Skip((1 - 1) * 15)
                                   .Take(15).ToList()
                                          }).OrderByDescending(x => x.CreatedDate).AsQueryable();

        UserItems = userResult.Where(x => x.IsDeleted == true).Skip((1 - 1) * 15)
                                   .Take(15).OrderByDescending(x => x.CreatedAt)
                              .AsQueryable();
        ServiceItems = serviceResult.Select(x => new ServiceModel
        {
            Id = x.Id,
            Name = x.Name,
            Color = x.Color,
            Price = x.Price,
            CalculationType = x.CalculationType
        }).Skip((1 - 1) * 15)
          .Take(15).AsQueryable();
        StateHasChanged();
    }

    private async Task NavigateUsers(int index = 1)
    {
        UserItems = null;
        var itemsPerPage = 15;

        UsersInDatabase = await usersRepository.FindAll(x => x.IsDeleted == true).ToListAsync();
        UsersPaginationCount = (int)Math.Ceiling((double)UsersInDatabase.Count / itemsPerPage);

        UserItems = UsersInDatabase.Skip((index - 1) * itemsPerPage)
                                   .Take(itemsPerPage)
                                   .AsQueryable();
        StateHasChanged();
    }

    private async Task NavigateBills(int index = 1)
    {
        BillItems = null;
        var itemsPerPage = 15;

        BillsInDatabase = await billsService.GetBillWithServicesAsync(isDeleted: true);
        BillsPaginationCount = (int)Math.Ceiling((double)BillsInDatabase.ToList().Count / itemsPerPage);
        var userLookup = UsersInDatabase.ToDictionary(user => user.Id);
        BillItems = BillsInDatabase.Where(x => x.PaidDate is not null)
                                          .Select(x => new BillWithMemberAndServicesInfo
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
                                              }).Skip((index - 1) * itemsPerPage)
                                                .Take(itemsPerPage).ToList()
                                          }).OrderByDescending(x => x.CreatedDate).AsQueryable();
        StateHasChanged();
    }
    private async Task NavigateServices(int index = 1)
    {
        ServiceItems = null;
        var itemsPerPage = 15;

        ServicesInDatabase = await servicesRepository.FindAll(x => x.IsDeleted).ToListAsync();
        ServicesPaginationCount = (int)Math.Ceiling((double)ServicesInDatabase.Count / itemsPerPage);

        ServiceItems = ServicesInDatabase.Select(x => new ServiceModel
        {
            Id = x.Id,
            Name = x.Name,
            Color = x.Color,
            Price = x.Price,
            CalculationType = x.CalculationType
        }).Skip((index - 1) * itemsPerPage)
          .Take(itemsPerPage).AsQueryable();
        StateHasChanged();
    }
    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied ID to clipboard");
    }
    private async Task Delete(BillWithMemberAndServicesInfo bill)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {bill.Id} will be permanently deleted?",
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

        billsRepository.Delete(entity);
        await billsRepository.SaveChangesAsync();
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "Record deleted permanently.");
    }
    private async Task Restore(BillWithMemberAndServicesInfo bill)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {bill.Id} will be restore?",
                                                                "Yes", "No",
                                                                "Do you want to restore this bill?");
        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await billsRepository.FindByIdAsync(bill.Id.ToString());
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Record not found in database, maybe it was deleted already.");
            return;
        }

        entity.IsDeleted = false;
        billsRepository.Update(entity);
        await billsRepository.SaveChangesAsync();
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "Record restored.");
    }
    private async Task DeleteUser(User userInfo)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {userInfo.Email} will be delete permanently?",
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

        await usersRepository.PermanentDelete(entity);
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "User deleted permanently !!!");
    }
    private async Task RestoreUser(User userInfo)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {userInfo.Email} will be restored?",
                                                                "Yes", "No",
                                                                "Do you want to restore this user?");

        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await usersRepository.FindByGuidAsync(userInfo.Id);
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "User not found in database, maybe it was deleted already.");
            return;
        }

        entity.IsDeleted = false;
        await usersRepository.UpdateAsync(entity);
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "User restored !!!");
    }
    private async Task DeleteService(ServiceModel service)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {service.Name} will be permanently deleted?",
                                                                "Yes", "No",
                                                                "Do you want to delete this service?");
        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await servicesRepository.FindByIdAsync(service.Id.ToString());
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Record not found in database, maybe it was deleted already.");
            return;
        }


        servicesRepository.Delete(entity);
        await servicesRepository.SaveChangesAsync();
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "Record deleted permanently.");
    }
    private async Task RestoreService(ServiceModel service)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Record {service.Name} will be restore?",
                                                                           "Yes", "No",
                                                                           "Do you want to restore this service?");

        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await servicesRepository.FindByIdAsync(service.Id.ToString());
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Record not found in database, maybe it was deleted already.");
            return;
        }

        entity.IsDeleted = false;
        servicesRepository.Update(entity);
        await servicesRepository.SaveChangesAsync();
        await RefreshAsync();
    }
    private string Sum(BillWithMemberAndServicesInfo item)
    {
        decimal amount = default(decimal)!;

        foreach (var service in item.Services)
        {
            switch (service.CalculationType)
            {
                case CalculationType.DirectAddition:
                    amount += service.Price;
                    break;
                case CalculationType.LotSizeMultiplication:
                    amount += service.Price * item.User!.LotSize;
                    break;
                default:
                    amount += service.Price;
                    break;
            }
        }
        return amount.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
    }
    private void HandleOnTabChange(FluentTab tab)
    {

    }
    private Appearance PageUsersButtonAppearance(int pageIndex)
        => CurrentUsersPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    private Appearance PageBillsButtonAppearance(int pageIndex)
        => CurrentBillsPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    private Appearance PageServicesButtonAppearance(int pageIndex)
    => CurrentBillsPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    #endregion

}
