namespace Blazor.Web;

public interface IBillsService
{
    Task<IEnumerable<BillWithServicesDTO>> GetBillWithServicesAsync(bool isDeleted = false, CancellationToken cancellationToken = default!);

    Task<IEnumerable<BillWithServicesDTO>> GetBillWithServicesByUserIdAsync(string userId,
                                                                            int lotSize,
                                                                            bool isDeleted = false,
                                                                            CancellationToken cancellationToken = default!);
}
