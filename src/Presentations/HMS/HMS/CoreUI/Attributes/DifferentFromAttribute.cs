using System.ComponentModel.DataAnnotations;

namespace HMS;

public class DifferentFromAttribute : ValidationAttribute
{
    private readonly string otherPropertyName;

    public DifferentFromAttribute(string otherPropertyName)
    {
        this.otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = value as string;

        var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(otherPropertyName);

        if (otherProperty == null)
        {
            return new ValidationResult($"Property {otherPropertyName} not found.");
        }

        var otherValue = otherProperty.GetValue(validationContext.ObjectInstance) as string;

        if (currentValue == otherValue)
        {
            return new ValidationResult($"The {validationContext.DisplayName} must be different from the {otherPropertyName}.");
        }

        return ValidationResult.Success;
    }
}
