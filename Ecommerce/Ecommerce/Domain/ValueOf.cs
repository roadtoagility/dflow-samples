
using System.Text.Json.Serialization;
using DFlow.Domain.Validation;

namespace Ecommerce.Domain;

public class ValueOf<TValue, TThis> : ValueOf.ValueOf<TValue, TThis>
    where TThis : ValueOf<TValue, TThis>, new()
{
    [JsonIgnore]
    private readonly ValidationResult _validationStatus = ValidationResult.Empty();
    [JsonIgnore]
    public ValidationResult ValidationStatus => _validationStatus;

    public static implicit operator TValue(ValueOf<TValue, TThis> d) => d.Value;
}