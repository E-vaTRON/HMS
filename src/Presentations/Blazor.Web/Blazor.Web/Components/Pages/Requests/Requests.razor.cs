using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using BlazorBootstrap;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Web;

public partial class Requests : AuthenticationComponentBase
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
    private IMediaService mediaService { get; set; } = null!;
    [Inject]
    private IRequestRepository requestRepository { get; set; } = null!;
    [Inject]
    private IBillsRepository billRepository { get; set; } = null!;
    [Inject]
    private IUsersRepository usersRepository { get; set; } = null!;
    [Inject]
    private ApplicationDbContext context { get; set; } = null!;
    [Inject]
    private IOptionsMonitor<List<SymptomGroupModel>> symptomsGroupConfig { get; set; } = null!;
    #endregion

    #region [ Properties ]
    public IQueryable<RequestModel>? Items { get; set; }
    public IEnumerable<RequestModel> SelectedItems = new List<RequestModel>();
    public IEnumerable<Request>? RequestsInDatabases { get; set; }
    PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
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

    private async Task RefreshAsync()
    {
        Items = null;
        var symptomsGroup = symptomsGroupConfig.CurrentValue;
        var bills = await billRepository.FindAll().ToListAsync();
        var convertedRequestId = bills.Select(x => x.RequestId).ToList();
        var requests = await requestRepository.FindAll().ToListAsync();
        var users = await usersRepository.FindAll().ToListAsync();
        var symptoms = symptomsGroup.SelectMany(symptom => symptom.Symptoms).ToList();

        RequestsInDatabases = requests.Where(x => !convertedRequestId.Contains(x.Id.ToString())).ToList();

        var requestData = RequestsInDatabases
            .Where(x => x.IsDeleted == false)
            .Select(x => new RequestModel
            {
                Id = x.Id,
                UserInfo = users.FirstOrDefault(y => y.Id == x.UserId)!,
                Symptoms = x.Symptoms,
                SymptomModels = x.Symptoms?.Split(',')
                                           .Select(id => symptoms.FirstOrDefault(s => s.Id.ToString() == id.Trim()))
                                           .ToList()!,
            }).OrderByDescending(x => x.CreatedDate);

        Items = requestData.AsQueryable();

        StateHasChanged();
    }

    private async Task AddRequestDialog()
    {
        var symptomList = new List<SymptomModel>();
        foreach (var items in symptomsGroupConfig.CurrentValue.Select(x => x.Symptoms).ToList())
        {
            foreach (var item in items)
            {
                symptomList.Add(item);
            }
        }
        AddRequestModel parameter = new()
        {
            SymptomGroups = symptomsGroupConfig.CurrentValue,
            Symptoms = symptomList
        };

        await DialogService.ShowDialogAsync<AddRequestDialog>(parameter, new()
        {
            Title = "Create new request",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleCreateRequestDialog(result)),
            Width = "600px",
            Height = "550px",
            TrapFocus = true,
            Modal = true,
            PrimaryActionEnabled = false
        });
    }

    private async Task HandleCreateRequestDialog(DialogResult result)
    {
        if (result.Cancelled)
        {
            ToastService.ShowToast(ToastIntent.Info, "Operation was cancelled.");
            return;
        }

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create announcement.");
            return;
        }

        var data = result.Data as AddRequestModel;
        var symptomIds = data.SelectedSymptoms.Select(x => x.Id).ToList();
        requestRepository.Add(new Request 
        { 
            UserId = data.SelectedUser?.Id, 
            Symptoms = string.Join(",", symptomIds) 
        });
        await requestRepository.SaveChangesAsync();

        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "New request created !!!");
    }

    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied to clipboard");
    }

    private async Task ConvertRequestToBill(RequestModel request)
    {
        ConvertRequestToBillModel parameter = new() { Request = request };

        await DialogService.ShowDialogAsync<ConvertRequestToBillDialog>(parameter, new()
        {
            Title = "Convert request to bill",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleConvertRequestToBillDialog(result)),
            Width = "400px",
            Height = "300px",
            TrapFocus = true,
            Modal = true,
        });
    }

    private async Task HandleConvertRequestToBillDialog(DialogResult result)
    {
        if (result.Cancelled)
        {
            ToastService.ShowToast(ToastIntent.Info, "Operation was cancelled.");
            return;
        }

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create announcement.");
            return;
        }

        await context.Database.BeginTransactionAsync();

        var data = result.Data as ConvertRequestToBillModel;
        if (data is null || data.Deadline is null || data.Request is null)
        {
            ToastService.ShowToast(ToastIntent.Info, "No deadline or request was provided.");
            return;
        }

        Bill bill = new()
        {
            UserId = data.Request.UserId,
            DrugIdAndQuantity = string.Empty,
            Deadline = data.Deadline.Value,
            PaidDate = null,
            ExcessAmount = default!,
            UnderPaidAmount = default!,
            CreatedDate = DateTime.Now,
            RequestId = data.Request.Id.ToString()
        };
        billRepository.Add(bill);

        var request = await requestRepository.FindByIdAsync(data.Request.Id.ToString());
        if (request is null)
        {
            ToastService.ShowToast(ToastIntent.Info, "This request had been converted");
            return;
        }
        request.IsDeleted = true;
        requestRepository.Update(request);

        await billRepository.SaveChangesAsync();
        await requestRepository.SaveChangesAsync();

        await context.Database.CommitTransactionAsync();

        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "New bill created !!!");
    }

    private async Task GoToPageAsync(int pageIndex)
    {
        await pagination.SetCurrentPageIndexAsync(pageIndex);
        //await RefreshAsync();
    }
    private Appearance PageButtonAppearance(int pageIndex)
        => pagination.CurrentPageIndex == pageIndex ? Appearance.Accent : Appearance.Neutral;
    private string? AriaCurrentValue(int pageIndex)
        => pagination.CurrentPageIndex == pageIndex ? "page" : null;
    private string AriaLabel(int pageIndex)
        => $"Go to page {pageIndex}";
    #endregion
}