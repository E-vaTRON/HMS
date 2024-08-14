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

public partial class Drugs : AuthenticationComponentBase
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
    private IDrugRepository drugRepository { get; set; } = null!;
    #endregion

    #region [ Properties ]

    public int CurrentPage { get; set; } = 1;
    public int PaginationCount { get; set; } = 0;
    public string ItemDescriptionFilter { get; set; } = string.Empty;
    public string AmountRadioChoice { get; set; } = default!;
    public string OrderDateRadioChoice { get; set; } = default!;
    public string QuantityRadioChoice { get; set; } = default!;
    public string PriceRadioChoice { get; set; } = default!;
    public string TotalAmount { get; set; } = string.Empty;
    public ICollection<Drug>? DrugsInDatabase { get; set; }
    public IQueryable<Drug>? Items { get; set; }
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
                                    string itemDescriptionFilter = "",
                                    string amountSorting = "1",
                                    string orderDateSorting = "1",
                                    string quantitySorting = "1",
                                    string priceSorting = "1")
    {
        Items = null;
        var itemsPerPage = 15;

        DrugsInDatabase = await drugRepository.FindAll().ToListAsync();
        PaginationCount = (int)Math.Ceiling((double)DrugsInDatabase.Count / itemsPerPage);

        var items = DrugsInDatabase.OrderByDescending(x => x.CreatedDate);

        if (!string.IsNullOrEmpty(itemDescriptionFilter))
            items = items.Where(x => x.Description!.ToLower().Contains(itemDescriptionFilter.ToLower()))
                         .OrderByDescending(x => x.CreatedDate);

        if (amountSorting != "1")
        {
            if (amountSorting == "2")
            {
                items = items.OrderBy(x => x.Price * x.Quantity);
            }
            else if (amountSorting == "3")
            {
                items = items.OrderByDescending(x => x.Price * x.Quantity);
            }
        }
        else AmountRadioChoice = "1";

        if (orderDateSorting != "1")
        {
            if (orderDateSorting == "2")
            {
                items = items.OrderBy(x => x.OrderDate);
            }
            else if (orderDateSorting == "3")
            {
                items = items.OrderByDescending(x => x.OrderDate);
            }
        }
        else OrderDateRadioChoice = "1";

        if (quantitySorting != "1")
        {
            if (quantitySorting == "2")
            {
                items = items.OrderBy(x => x.Quantity);
            }
            else if (quantitySorting == "3")
            {
                items = items.OrderByDescending(x => x.Quantity);
            }
        }
        else QuantityRadioChoice = "1";

        if (priceSorting != "1")
        {
            if (priceSorting == "2")
            {
                items = items.OrderBy(x => x.Price);
            }
            else if (priceSorting == "3")
            {
                items = items.OrderByDescending(x => x.Price);
            }
        }
        else PriceRadioChoice = "1";

        Items = items.Skip((index - 1) * itemsPerPage)
                                  .Take(itemsPerPage)
                                  .AsQueryable();
        TotalAmount = items.Sum(x => (x.Quantity * x.Price)).ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
        StateHasChanged();
    }
    private async Task AddDrugDialog()
    {
        AddDrugModel drug = new()
        {
            Id = Guid.NewGuid(),
            Supplier = string.Empty,
            CreatedDate = DateTime.Now,
            Quantity = 0,
            Price = default(decimal),
            Description = string.Empty,
            OrderDate = DateTime.Now
        };
        await DialogService.ShowDialogAsync<AddDrugDialog>(drug, new DialogParameters()
        {
            Title = $"Create new drug",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result)),
            Width = "600px",
            Height = "550px",
            TrapFocus = true,
            Modal = true,
            PrimaryAction = "Add drug",
            PrimaryActionEnabled = false
        });
    }
    private async Task EditDrugDiaglog(Drug drug)
    {
        AddDrugModel editDrug = new()
        {
            Id = drug.Id,
            Supplier = drug.Supplier,
            CreatedDate = drug.CreatedDate,
            OrderDate = drug.OrderDate,
            Quantity = drug.Quantity,
            Price = drug.Price,
            Description = drug.Description,
        };
        await DialogService.ShowDialogAsync<AddDrugDialog>(editDrug, new DialogParameters()
        {
            Title = $"Edit {drug.Description ?? string.Empty}",
            Alignment = HorizontalAlignment.Center,
            OnDialogResult = EventCallback.Factory.Create<DialogResult>(this, result => HandleDialog(result, isUpdate: true)),
            Width = "700px",
            Height = "450px",
            TrapFocus = true,
            PrimaryAction = "Update drug",
            Modal = true,
            PrimaryActionEnabled = false
        });
    }
    private async Task HandleDialog(DialogResult result, bool isUpdate = false)
    {
        string state = "";
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create an drug");
            return;
        }

        AddDrugModel? drug = result.Data as AddDrugModel;

        if (drug is null
            || string.IsNullOrEmpty(drug.Supplier)
            || string.IsNullOrWhiteSpace(drug.Supplier)
            || string.IsNullOrEmpty(drug.Description)
            || string.IsNullOrWhiteSpace(drug.Description)
            || drug.Price == 0
            || !drug.IsItemDescriptionValid
            || !drug.IsQuantityValid
            || !drug.IsPriceValid
            || !drug.IsOrderDateValid)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create an drug");
            return;
        }

        if (isUpdate)
        {
            var updatedDrug = await drugRepository.FindByIdAsync(drug.Id.ToString());
            if (updatedDrug is null)
            {
                ToastService.ShowToast(ToastIntent.Error, "Something wrong");
                return;
            }

            updatedDrug.Supplier = drug.Supplier;
            updatedDrug.Description = drug.Description;
            updatedDrug.Quantity = drug.Quantity;
            updatedDrug.Price = drug.Price;
            updatedDrug.CreatedDate = drug.CreatedDate;
            updatedDrug.OrderDate = drug.OrderDate;

            drugRepository.Update(updatedDrug);
            state = "Update";
        }

        else
        {
            if (drug.DrugFiles is not null && drug.DrugFiles.Any())
            {
                var uploadedDrugInfomation = await mediaService.UploadFileAsync(drug.DrugFiles!.FirstOrDefault()!,
                                                                                    MediaTypeDTO.Photo);
                if (uploadedDrugInfomation is not null)
                {
                    drug.FileReference = uploadedDrugInfomation.MediaUrl;
                }
            }

            drugRepository.Add(drug);
            state = "Add";
        }

        await drugRepository.SaveChangesAsync();
        ToastService.ShowToast(ToastIntent.Success, $"{state} successfully");

        await RefreshAsync();
    }
    private async Task Delete(Drug drug)
    {

        var dialog = await DialogService.ShowConfirmationAsync($"Record {drug.Id} will be remove?",
                                                                "Yes", "No",
                                                                "Do you want to delete this record?");
        var result = await dialog.Result;
        if (result.Cancelled)
            return;

        var entity = await drugRepository.FindByIdAsync(drug.Id.ToString());
        if (entity is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Record not found in database, maybe it was deleted already.");
            return;
        }

        drugRepository.Delete(entity);
        await drugRepository.SaveChangesAsync();
        await RefreshAsync();
        ToastService.ShowToast(ToastIntent.Success, "Record deleted successfully.");
    }
    private async Task CopyToClipboard(string text)
    {
        await JSRuntime.InvokeVoidAsync("clipboardInterop.copyText", text);
        ToastService.ShowToast(ToastIntent.Success, "Copied to clipboard");
    }
    private Appearance PageButtonAppearance(int pageIndex)
        => CurrentPage == pageIndex ? Appearance.Accent : Appearance.Neutral;
    async Task HandleTextFilterInput()
    => await RefreshAsync(CurrentPage,
                          ItemDescriptionFilter,
                          AmountRadioChoice,
                          OrderDateRadioChoice,
                          QuantityRadioChoice,
                          PriceRadioChoice);

    async Task SelectedChoice()
        => await RefreshAsync(CurrentPage,
                          ItemDescriptionFilter,
                          AmountRadioChoice,
                          OrderDateRadioChoice,
                          QuantityRadioChoice,
                          PriceRadioChoice);

    async Task ClearFormats()
    {
        ItemDescriptionFilter = string.Empty;
        AmountRadioChoice = "1";
        OrderDateRadioChoice = "1";
        QuantityRadioChoice = "1";
        PriceRadioChoice = "1";
        await RefreshAsync(CurrentPage,
                          ItemDescriptionFilter,
                          AmountRadioChoice,
                          OrderDateRadioChoice,
                          QuantityRadioChoice,
                          PriceRadioChoice);
    }
    #endregion

    #region [ Excel ]

    async Task<byte[]> GenerateExcel(ICollection<Drug> items)
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
                Cell header1 = new Cell() { CellReference = "A1", CellValue = new CellValue("Item description"), DataType = CellValues.String };
                headerRow.Append(header1);
                Cell header2 = new Cell() { CellReference = "B1", CellValue = new CellValue("Date"), DataType = CellValues.String };
                headerRow.Append(header2);
                Cell header3 = new Cell() { CellReference = "C1", CellValue = new CellValue("Quantity"), DataType = CellValues.String };
                headerRow.Append(header3);
                Cell header4 = new Cell() { CellReference = "D1", CellValue = new CellValue("Price"), DataType = CellValues.String };
                headerRow.Append(header4);
                Cell header5 = new Cell() { CellReference = "E1", CellValue = new CellValue("Amount"), DataType = CellValues.String };
                headerRow.Append(header5);

                // Add user data
                foreach (var item in items)
                {
                    Row dataRow = new Row();
                    dataRow.Append(CreateTextCell(item.Description ?? string.Empty));
                    dataRow.Append(CreateTextCell(item.OrderDate.HasValue ? item.OrderDate.Value.ToString("dd/MM/yy") : string.Empty));
                    dataRow.Append(CreateTextCell(item.Quantity.ToString()));
                    dataRow.Append(CreateTextCell(item.Price.ToString("N2")));
                    dataRow.Append(CreateTextCell((item.Price * item.Quantity).ToString("N2")));
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
        if (DrugsInDatabase is null)
            return;

        string fileName = "HMS drugs";

        byte[] excelData = await GenerateExcel(DrugsInDatabase);
        await JSRuntime.InvokeVoidAsync("saveAsFile",
                                        fileName,
                                        Convert.ToBase64String(excelData),
                                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    async Task UploadExcel()
    {
        var data = new UploadDrugsExcelModel();
        await DialogService.ShowDialogAsync<UploadDrugExcelDialog>(data, new DialogParameters()
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

    async Task HandleExcelDialog(DialogResult result)
    {
        if (result.Cancelled)
            return;

        if (result.Data is null)
        {
            ToastService.ShowToast(ToastIntent.Error, "Not enough information to create an drug");
            return;
        }

        var file = result.Data as UploadDrugsExcelModel;
        if (file == null)
        {
            return;
        }
        drugRepository.AddRange(file.Drugs);
        await drugRepository.SaveChangesAsync();
        await this.RefreshAsync();
    }
    #endregion
}
