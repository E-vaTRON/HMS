namespace Blazor.Web;

public record BillDTO(string? id,
                      string userId,
                      DateTime deadLine,
                      DateTime? paidDate,
                      decimal excessAmount,
                      decimal underPaidAmount,
                      string serviceIds);