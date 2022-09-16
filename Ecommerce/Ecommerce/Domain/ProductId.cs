// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.SymbolStore;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Validation;

namespace Ecommerce.Domain;

public class ProductId: ValueOf<Guid, ProductId>
{
    public static ProductId Empty => ProductId.From(Guid.Empty);
    public static ProductId From(string productId)
    {
        Guid id = Guid.Empty;
        if (Guid.TryParse(productId, out id) == false)
        {
            var result = From(Empty.Value);
            result.ValidationStatus.Append(Failure
                .For("ProductId",$"O id do produto '{productId}' informado não é válido."));
            return result;
        }
        return From(id);
    }

    public static ProductId NewId()
    {
        return From(Guid.NewGuid());
    }
    
    protected override void Validate()
    {
        if (Value.Equals(Guid.Empty))
        {
            
        }
    }
}