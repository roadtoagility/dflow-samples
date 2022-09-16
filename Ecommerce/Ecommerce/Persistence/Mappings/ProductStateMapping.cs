// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ecommerce.Persistence.State;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Mappings;

public class ProductStateMapping : IEntityTypeConfiguration<ProductState>
{
    public void Configure(EntityTypeBuilder<ProductState> builder)
    {

        builder.Property(e=> e.ProductStateId).ValueGeneratedNever().IsRequired();
        builder.HasKey(e=> e.ProductStateId);
        
        builder.Property(e=> e.Name);
        builder.Property(e=> e.Description);
        builder.Property(e=> e.Weight);
        
        builder.Property(e => e.IsDeleted);
        builder.HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false);
        builder.Property(e => e.RowVersion);
    }
}
