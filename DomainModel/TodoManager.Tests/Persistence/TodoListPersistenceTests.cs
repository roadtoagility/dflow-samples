// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using DFlow.Domain.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using TodoManager.Tests.Domain.DataProviders;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence;
using TodoManagerApp.Persistence.ExtensionMethods;
using TodoManagerApp.Persistence.Repositories;
using Xunit;

namespace TodoManager.Tests.Persistence;

public class DatabaseTesting
{
    
}

public class TodoListPersistenceTests
{

    [Theory]
    [ClassData(typeof(ValidTodoListProvider))]
    public void CreateValidTodoList(TodoListName name, TodoListId id, List<Todo> todos, VersionId version, TodoList expected)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TodoAppDbContext>();
        optionsBuilder.UseSqlite("Data Source=todoapp.sqlite3;");
        var dbContext = new TodoAppDbContext(optionsBuilder.Options);
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        
        dbContext.Add(new TodoList(name,id,todos.ToImmutableList(),version).ToTodoListState());
        dbContext.SaveChanges();

        var rep = new TodoListRepository(dbContext);

        var result = rep.FindAsync(e => true, CancellationToken.None)
            .GetAwaiter().GetResult();
        
        Assert.Equal(expected, result[0]);
    }
}