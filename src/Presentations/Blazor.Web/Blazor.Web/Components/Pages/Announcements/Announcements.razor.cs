using Blazor.Web.Components.Pages.AuthenticationComponentBase;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Web;

public partial class Announcements : AuthenticationComponentBase
{

    #region [ Fields ]

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ILocalStorageService LocalStorage { get; set; } = null!;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    private IToastService ToastService { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private IMediaService mediaService { get; set; } = null!;

    [Inject]
    private IMediaRepository mediaRepository { get; set; } = null!;

    [Inject]
    private IAnnouncementsRepository announcementsRepository { get; set; } = null!;
    #endregion

    #region [ Properties ]
    public int CurrentPage { get; set; } = 1;
    public int PaginationCount { get; set; } = 0;
    public string SearchText { get; set; } = string.Empty;
    public ICollection<Announcement>? AnnouncementsInDatabase { get; set; }
    public IQueryable<AnnouncementModel>? Items { get; set; }
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

        AnnouncementsInDatabase = await announcementsRepository.FindAll().ToListAsync();
        PaginationCount = (int)Math.Ceiling((double)AnnouncementsInDatabase.Count / itemsPerPage);
        Items = AnnouncementsInDatabase.Select(x => new AnnouncementModel
        {
            Id = x.Id,
            Title = x.Title,
            Content = x.Content,
            CreatedDate = x.CreatedDate,
            FileUrl = x.FileUrl,
        }).Skip((index - 1) * itemsPerPage)
          .Take(itemsPerPage)
          .AsQueryable();
        StateHasChanged();
    }
    private async Task AddAnnouncementDialog()
    {
        AnnouncementModel announcementModel = new()
        {
            Content = string.Empty,
            FileUrl = string.Empty,
        };

        await DialogService.ShowDialogAsync<AnnouncementDialog>(announcementModel, new DialogParameters()
        {
            Title = $"Create new announcement",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result)),
            Width = "600px",
            Height = "550px",
            TrapFocus = true,
            Modal = true,
            PrimaryActionEnabled = false
        });
    }
    private async Task HandleDialog(DialogResult result)
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

        SystemMediaDTO? file = null;
        var announcement = result.Data as AnnouncementModel;
        if (announcement is null
            || string.IsNullOrEmpty(announcement.Title)
            || string.IsNullOrWhiteSpace(announcement.Title)
            || !announcement.IsTitleValid
            || !announcement.IsDescriptionValid)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create announcement.");
            return;
        }

        if (announcement.AttachedFiles is not null && announcement.AttachedFiles.Any())
        {
            file = await mediaService.UploadFileAsync(announcement.AttachedFiles!.FirstOrDefault()!,
                                                      MediaTypeDTO.File);
        }



        if (file is not null && file.MediaUrl is not null)
        {
            Media media = new()
            {
                MediaUrl = file.MediaUrl,
                Type = MediaType.File,
                CreatedDate = DateTime.UtcNow
            };
            mediaRepository.Add(media);
            await mediaRepository.SaveChangesAsync();
        }

        Announcement entity = new()
        {
            Title = announcement.Title,
            Content = announcement.Content,
            FileUrl = file?.MediaUrl,
            CreatedDate = DateTime.Now
        };

        announcementsRepository.Add(entity);
        await announcementsRepository.SaveChangesAsync();

        await RefreshAsync();
    }
    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied to clipboard");
    }
    private async Task Delete(Announcement announcement)
    {
        if (announcement is null) return;

        var dialog = await DialogService.ShowConfirmationAsync($"Record {announcement.Id} will be move to delete?",
                                                                "Yes", "No",
                                                                "Do you want to delete this announcement?");

        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await announcementsRepository.FindByIdAsync(announcement.Id.ToString());
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Announcement not found in database, maybe it was deleted already.");
            return;
        }


        if (announcement.FileUrl is not null)
            await mediaService.DeleteFileAsync(announcement.FileUrl, true);

        announcementsRepository.Delete(entity);
        await announcementsRepository.SaveChangesAsync();
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, $"Annoucement deleted to archive !!!");
    }
    private Appearance PageButtonAppearance(int pageIndex)
        => CurrentPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    #endregion
}
