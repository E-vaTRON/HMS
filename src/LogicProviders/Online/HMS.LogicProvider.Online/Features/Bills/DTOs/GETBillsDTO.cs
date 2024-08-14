namespace HMS.LogicProvider;

public record GETBillsDTO(string id, 
                          string userId, 
                          DateTime deadline,
                          DateTime? paidDate,
                          decimal excessAmount,
                          decimal underPaidAmount,
                          DateTime createdDate);

