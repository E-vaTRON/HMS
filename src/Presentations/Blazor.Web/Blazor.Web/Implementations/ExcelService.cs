using ExcelDataReader;
using System.Data;

namespace Blazor.Web;

public class ExcelService : IExcelService
{
    public async Task<DataTable?> GetDataTableAsync(IFormFile file)
    {
        var tempPath = Path.GetTempFileName();

        using (FileStream stream = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.RandomAccess | FileOptions.DeleteOnClose))
        {
            await file.CopyToAsync(stream, default);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            {
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = reader.AsDataSet(conf);
                var dataTable = dataSet.Tables[0];

                return dataTable;
            }
        }
    }
}
