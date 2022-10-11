// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Collections.Immutable;
using DFlow.Domain.BusinessObjects;
using TodoManagerApp.Domain;
using Xunit;

namespace TodoManager.Tests.Domain;

public class TodoListTests
{
    // [Theory]
    // [ClassData(typeof(TodoListProvider))]
    public void Create_Valid_TodoList(TodoListName listName, TodoListId identity, IReadOnlyList<Todo> todos, VersionId version, TodoList expected)
    {
        var list = new TodoList(listName,identity, todos.ToImmutableList(), version);
        Assert.Equal(expected,list);
    }
}