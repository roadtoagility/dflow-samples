using DFlow.BusinessObjects;
using DFlow.Validation;

namespace DomainModel.SimpleApp.Domain;

public class ProductName: ValueOf<string, ProductName>
{
    protected override void Validate()
    {
        if (string.IsNullOrEmpty(Value) ||
            string.IsNullOrWhiteSpace(Value))
        {
            ValidationStatus.Append(Failure.For(
                nameof(ProductName)
                ,"O nome é requerido."));
            
        }
    }
}