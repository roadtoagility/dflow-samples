// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.Command;
using Ecommerce.Domain;

namespace Ecommerce.Business;

public class CreateProduct: BaseCommand
{
    public ProductDescription Description { get; }
 
    public ProductName Name { get; }
    
    public ProductWeight Weight { get; }
    
    public CreateProduct(string description, string name, float weight)
    {
        Name = ProductName.From(name);
        Weight = ProductWeight.From(weight);
        Description = ProductDescription.From(description);
    }
}