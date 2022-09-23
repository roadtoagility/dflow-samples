// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ecommerce.Domain.Events;

public class ProductCreatedEvent : DomainEvent
{
    public ProductCreatedEvent(ProductId id, ProductName name, ProductDescription description
        , ProductWeight weight)
        : base(DateTime.Now)
    {
        Id = id.Value;
        Name = name.Value;
        Description = description.Value;
        Weight = weight.Value;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public double Weight { get; }

    public static ProductCreatedEvent For(Product product)
    {
        return new (
            product.Identity, 
            product.Name,
            product.Description, 
            product.Weight);
    }
}