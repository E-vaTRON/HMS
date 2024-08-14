using System.Data;

namespace Blazor.Web;

public interface IExcelService
{
    Task<DataTable?> GetDataTableAsync(IFormFile file);
}
