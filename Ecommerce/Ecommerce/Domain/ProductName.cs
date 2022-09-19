// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Validation;

namespace Ecommerce.Domain;

public class ProductName : ValueOf<string, ProductName>
{
    public static ProductName Empty => From(string.Empty);

    protected override void Validate()
    {
        if (string.IsNullOrEmpty(Value) ||
            string.IsNullOrWhiteSpace(Value))
        {
            ValidationStatus.Append(Failure
                .For("ProductName", "O nome do produto é de preenchimento obrigatório."));
        }
    }
}