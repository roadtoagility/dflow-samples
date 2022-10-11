// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence.Mappings;

public class TodoListStateMapping : IEntityTypeConfiguration<TodoListState>
{
    public void Configure(EntityTypeBuilder<TodoListState> builder)
    {

        builder.Property(e=> e.Id).ValueGeneratedNever().IsRequired();
        builder.HasKey(e=> e.Id);
        builder.OwnsMany(e => e.Todos);
        builder.Property(e => e.IsDeleted);
        builder.HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false);
        builder.Property(e => e.RowVersion);
    }
}
