// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.EntityFrameworkCore;
using TodoManagerApp.Persistence.Mappings;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence;

public sealed class TodoAppDbContext: SoftDeleteDbContext
{
    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> contextOptions)
        : base(contextOptions)
    {
        this.Database.EnsureCreated();
        this.Database.EnsureDeleted();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new TodoListStateMapping().Configure(modelBuilder.Entity<TodoListState>());
    }
}