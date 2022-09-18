// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ecommerce.Persistence.Mappings;
using Ecommerce.Persistence.State;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence;

public sealed class EcommerceAppDbContext: SoftDeleteDbContext
{
    public EcommerceAppDbContext(DbContextOptions<EcommerceAppDbContext> contextOptions)
        : base(contextOptions)
    {
        this.Database.EnsureCreated();
        this.Database.EnsureDeleted();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ProductStateMapping().Configure(modelBuilder.Entity<ProductState>());
    }
}