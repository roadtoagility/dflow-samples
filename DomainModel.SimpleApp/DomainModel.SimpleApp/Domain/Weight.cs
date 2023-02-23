// // Copyright (C) 2023  Road to Agility

using DFlow.BusinessObjects;
using DFlow.Domain.Validation;

namespace DomainModel.SimpleApp.Domain;

public class Weight: DFlow.Domain.BusinessObjects.ValueOf<double, Weight>
{
    protected override void Validate()
    {
        if (Value < 0)
        {
            // throw new ArgumentException("O Peso não pode ser negativo",
            //     nameof(Weight));
            ValidationStatus.Append(Failure.For("Weight",""));
        }
    }
}