using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Web;

public partial class BuildingStructure : AuthenticationComponentBase
{

    #region [ Fields ]

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    private ApplicationDbContext context { get; set; } = null!;
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;
    [Inject]
    private IToastService ToastService { get; set; } = null!;
    [Inject]
    private IDialogService DialogService { get; set; } = null!;
    [Inject]
    private IMediaService mediaService { get; set; } = null!;
    [Inject]
    private IUsersRepository usersRepository { get; set; } = null!;
    [Inject]
    private IServicesRepository servicesRepository { get; set; } = null!;
    [Inject]
    private IdentityDbContext identityDbContext { get; set; } = null!;
    [Inject]
    private IOptionsMonitor<List<FloorModel>> floorsConfig { get; set; } = null!;
    #endregion

    #region [ Properties ]
    public ICollection<User>? UsersInDatabase { get; set; }
    public List<FloorModel> Floors { get; set; } = new List<FloorModel>();
    public IQueryable<Drug>? Items { get; set; }
    public List<RoomEvent> RoomEvents { get; set; } = new List<RoomEvent>();
    public DateTime? SelectedDate { get; set; } = DateTime.Today;
    #endregion

    #region [ Parameter ]
    [Parameter]
    public List<Service> Services { get; set; } = new List<Service>();
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
        var random = new Random();
        Floors = floorsConfig.CurrentValue;
        Services = await servicesRepository.FindAll(x => x.IsDeleted == false).ToListAsync();
        UsersInDatabase = await usersRepository.FindAll(x => x.IsDeleted == false).ToListAsync();

        var currentUserGuid = await LocalStorage.GetItemAsync<string>("userId");
        if (currentUserGuid is null)
            return;

        var usersIdWithAdminRole = await identityDbContext.UserRoles
                                                .Where(x => x.RoleId == MasterDataConstants.AdminRoleId)
                                                .Select(x => x.UserId)
                                                .ToListAsync();

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
                            EndDate = e.EndDate,
                            UserIds = e.UserIds
                        })
                        .ToList();

                    // Assign the list of events to the room's Events property
                    room.Events = eventsForRoom;
                }
            }
        }

        List<RoomEvent> roomEventsFake = new List<RoomEvent>
        {
            new RoomEvent
            {
                RoomId = "203",
                BillId = "",
                ServiceId = "2100e110-bc66-4dcc-b662-09f82a8b8bd9",
                UserIds = "c54274f0-5603-4f37-a400-c17ac38a9061",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            },
            new RoomEvent
            {
                RoomId = "204",
                BillId = "",
                ServiceId = "2100e110-bc66-4dcc-b662-09f82a8b8bd9",
                UserIds = "c54274f0-5603-4f37-a400-c17ac38a9061",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(3)
            },
            new RoomEvent
            {
                RoomId = "203",
                BillId = "",
                ServiceId = "2100e110-bc66-4dcc-b662-09f82a8b8bd9",
                UserIds = "c54274f0-5603-4f37-a400-c17ac38a9062",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            },
            new RoomEvent
            {
                RoomId = "205",
                BillId = "",
                ServiceId = "2100e110-bc66-4dcc-b662-09f82a8b8bd9",
                UserIds = "c54274f0-5603-4f37-a400-c17ac38a9062",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(3)
            },
            new RoomEvent
            {
                RoomId = "205",
                BillId = "",
                ServiceId = "2100e110-bc66-4dcc-b662-09f82a8b8bd9",
                UserIds = "c54274f0-5603-4f37-a400-c17ac38a9063",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            },
            new RoomEvent
            {
                RoomId = "302",
                BillId = "",
                ServiceId = "2100e110-bc66-4dcc-b662-09f82a8b8bd9",
                UserIds = "c54274f0-5603-4f37-a400-c17ac38a9063",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(3)
            },
        };
        if (usersIdWithAdminRole.Contains(currentUserGuid))
        {
            foreach (var roomEvent in roomEvents.Where(x => x.UserIds != null && x.UserIds.Split(',').Select(s => s.Trim()).ToList().Contains(currentUserGuid)).OrderBy(x => x.StartDate).ToList())
            {
                if (roomEvent is not null)
                {
                    RoomEvents.Add(roomEvent);
                }
            }
        }
        //else
        //{
        //    RoomEvents = roomEvents.OrderBy(x => x.StartDate).ToList();
        //}

        StateHasChanged();
    }
    private async Task OpenRoomInfoDialog(RoomModel roomInfo)
    {
        List<RoomEventDialog> events = new List<RoomEventDialog>();

        var serviceIds = roomInfo.Events.Select(x => x.ServiceId).ToList();
        // Get all services with the collected service IDs
        var serviceDetails = context.Services
            .Where(service => serviceIds.Contains(service.Id.ToString()))
            .ToList();

        foreach (var item in roomInfo.Events)
        {
            var serviceDetail = serviceDetails.FirstOrDefault(x => x.Id == Guid.Parse(item.ServiceId));
            if (serviceDetail is null)
                continue;

            var roomEventDetail = roomInfo.Events.FirstOrDefault(x => x.ServiceId == item.ServiceId);
            if (roomEventDetail is null)
                continue;

            events.Add(new()
            {
                EventTitle = serviceDetail.Name,
                RoomId = roomEventDetail.RoomId,
                BillId = roomEventDetail.BillId,
                ServiceId = item.ServiceId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Participants = UsersInDatabase?.Where(x => item.UserIds is not null && item.UserIds.Split(',').Select(s => s.Trim()).ToList().Contains(x.Id))?.ToList() ?? [],
            });
        }

        RoomInfoDetailModel parameter = new()
        {
            Name = roomInfo.Name,
            Events = events.OrderBy(x => x.CreatedDate).ToList(),
        };

        await DialogService.ShowDialogAsync<RoomInfoDetailDialog>(parameter, new DialogParameters()
        {
            Title = $"{parameter.Name}",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result)),
            Width = "800px",
            Height = "550px",
            TrapFocus = true,
            Modal = true,
        });
    }

    private async Task HandleDialog(DialogResult result, bool isUpdate = false)
    {
    }
    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied to clipboard");
    }

    private string GetButtonColor(bool isBusy)
    {
        return !isBusy ? "#5cdc5c" : "#ef5858";
    }
    #endregion
}

public class FloorInfo
{
    public string FloorName { get; set; } = string.Empty;
    public ICollection<RoomInfoDetailModel> RoomInfos { get; set; } = new List<RoomInfoDetailModel>();
}