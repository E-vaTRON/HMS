using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Blazor.Web;

public partial class Services : AuthenticationComponentBase
{

    #region [ Fields ]

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    private IDialogService DialogService { get; set; } = null!;
    [Inject]
    private IToastService ToastService { get; set; } = null!;

    [Inject]
    private IServicesRepository servicesRepository { get; set; } = null!;
    #endregion

    #region [ Properties ]
    public int CurrentPage { get; set; } = 1;
    public int PaginationCount { get; set; } = 0;
    public string SearchText { get; set; } = string.Empty;
    public ICollection<Service>? ServicesInDatabase { get; set; }
    public IQueryable<ServiceModel>? Items { get; set; }
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

    private async Task RefreshAsync(int index = 1)
    {
        Items = null;
        var itemsPerPage = 15;

        ServicesInDatabase = await servicesRepository.FindAll().ToListAsync();
        PaginationCount = (int)Math.Ceiling((double)ServicesInDatabase.Count / itemsPerPage);
        Items = ServicesInDatabase.Where(x => !x.IsDeleted).Select(x => new ServiceModel
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
    private async Task AddServiceDialog()
    {
        ServiceModel service = new()
        {
            Name = string.Empty,
            Price = 1,
            Color = "#627ade",
            CalculationType = CalculationType.DirectAddition
        };
        await DialogService.ShowDialogAsync<AddServiceDialog>(service, new DialogParameters()
        {
            Title = $"Create new service",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result)),
            Width = "300px",
            Height = "550px",
            TrapFocus = true,
            Modal = true,
            PrimaryActionEnabled = false
        });
    }

    private async Task EditServiceDialog(ServiceModel service)
    {
        await DialogService.ShowDialogAsync<AddServiceDialog>(service, new DialogParameters()
        {
            Title = $"Edit service",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result, isUpdate: true)),
            Width = "300px",
            Height = "550px",
            TrapFocus = true,
            PrimaryAction = "Update service",
            Modal = true,
            PrimaryActionEnabled = false
        });
    }

    private async Task Delete(ServiceModel service)
    {
        var dialog = await DialogService.ShowConfirmationAsync($"Service {service.Name} will be move to archive?",
                                                                "Yes", "No",
                                                                "Do you want to delete this service?");

        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var deleteService = await servicesRepository.FindByIdAsync(service.Id.ToString());
        if (deleteService is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Something wrong");
            return;
        }

        deleteService.IsDeleted = true;
        servicesRepository.Update(deleteService);
        await servicesRepository.SaveChangesAsync();
        ToastService.ShowToast(ToastIntent.Success, "Delete successfully");
        await RefreshAsync();
    }
    private async Task HandleDialog(DialogResult result, bool isUpdate = false)
    {
        string state = "";
        if (result.Cancelled)
            return;

        if (result.Data is null)
            return;

        ServiceModel? data = result.Data as ServiceModel;

        if (data is null
            || string.IsNullOrEmpty(data.Name)
            || string.IsNullOrWhiteSpace(data.Name)
            || data.Price == 0
            || !data.IsPriceValid
            || !data.IsColorValid
            || !data.IsNameNotDuplicated)
        {
            ToastService.ShowToast(ToastIntent.Error, "Wrong information to create a service");
            return;
        }

        if (isUpdate)
        {
            var updateService = await servicesRepository.FindByIdAsync(data!.Id.ToString());
            if (updateService is null)
            {
                ToastService.ShowToast(ToastIntent.Error, "Something wrong");
                return;
            }

            updateService.Name = data!.Name;
            updateService.Price = data!.Price;
            updateService.Color = data!.Color;
            updateService.CalculationType = data!.UICalculationType switch
            {
                "directadd" => CalculationType.DirectAddition,
                "monthlyorpenalty" => CalculationType.LotSizeMultiplication,
                _ => CalculationType.DirectAddition
            };
            servicesRepository.Update(updateService);
            state = "Update";
        }
        else
        {
            Service service = new()
            {
                Name = data!.Name,
                Price = data!.Price,
                Color = data!.Color,
                CalculationType = data!.UICalculationType switch
                {
                    "directadd" => CalculationType.DirectAddition,
                    "monthlyorpenalty" => CalculationType.LotSizeMultiplication,
                    _ => CalculationType.DirectAddition
                }
            };
            servicesRepository.Add(service);
            state = "Add";
        }

        await servicesRepository.SaveChangesAsync();
        ToastService.ShowToast(ToastIntent.Success, $"{state} successfully");

        await RefreshAsync();
    }
    private bool DisableEditButton(ServiceModel service)
    {
        string serviceId = service.Id.ToString(); // Assuming Id is a Guid

        return serviceId == MasterDataConstants.AdminRoleId
            || MasterDataConstants.ServiceIds.Contains(serviceId);
    }
    private Appearance PageButtonAppearance(int pageIndex)
        => CurrentPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    #endregion
}
