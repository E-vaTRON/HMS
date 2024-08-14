namespace HMS.LogicProvider;

public record GETBillsWithServicesDTO(string id,
                                      string userId,
                                      DateTime deadline,
                                      DateTime? paidDate,
                                      decimal excessAmount,
                                      decimal underPaidAmount,
                                      DateTime createdDate,
                                      GETBillServiceDTO[] services);

public record GETBillServiceDTO(string id, 
                                string name, 
                                string color, 
                                decimal price,
                                int quantity,
                                CalculationType calculationType);

public enum CalculationType
{
    DirectAddition,
    LotSizeMultiplication,
}

