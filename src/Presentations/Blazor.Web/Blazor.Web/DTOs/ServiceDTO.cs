namespace Blazor.Web;

public record ServiceDTO(string? id, 
                         string name, 
                         string color, 
                         decimal price,
                         ServiceCalculationTypeDTO calculationType);

public enum ServiceCalculationTypeDTO
{

    DirectAddition,
    LotSizeMultiplication,
}