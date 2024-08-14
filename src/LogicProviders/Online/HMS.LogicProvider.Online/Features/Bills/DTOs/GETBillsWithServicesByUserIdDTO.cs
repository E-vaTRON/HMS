namespace HMS.LogicProvider;

public record GETBillsWithServicesByUserIdDTO(string id,
                                              string userId,
                                              int lotSize,
                                              DateTime deadline,
                                              DateTime? paidDate,
                                              decimal excessAmount,
                                              decimal underPaidAmount,
                                              DateTime createdDate,
                                              GETBillServiceDTO[] services);

public record GETBillServiceByUserIdDTO(string id, 
                                string name, 
                                string color, 
                                decimal price,
                                int quantity,
                                CalculationType calculationType);

