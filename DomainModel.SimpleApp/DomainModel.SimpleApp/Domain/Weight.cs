// // Copyright (C) 2023  Road to Agility

using DFlow.BusinessObjects;
using DFlow.Validation;

namespace DomainModel.SimpleApp.Domain;

public class Weight: ValueOf<double, Weight>
{
    protected override void Validate()
    {
        if (Value < 0)
        {
            ValidationStatus.Append(Failure.For("Weight",""));
        }
    }
}