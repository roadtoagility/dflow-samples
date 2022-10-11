// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using TodoManager.Tests.Domain.DataProviders;
using TodoManagerApp.Domain;
using Xunit;

namespace TodoManager.Tests.Domain;

public class TodoListNameTests
{
    [Theory]
    [ClassData(typeof(ValidListNameProvider))]
    public void Create_Valid_TodoListName(string input, TodoListName expected)
    {
        var listName = TodoListName.From(input);
        
        Assert.Equal(expected,listName);
    }

    [Fact]
    public void Create_Valid_TodoListId()
    {
        int input = 1;
        var id = TodoListId.From(input);
        Assert.Equal(TodoListId.From(input),id);
    }

    
    [Theory]
    [ClassData(typeof(InvalidListNameProvider))]
    public void Create_Invalid_TodoListName(string input, bool expected, int failureCount)
    {
        var listName = TodoListName.From(input);
        Assert.Equal(expected,listName.ValidationStatus.IsValid);
        Assert.Equal(failureCount,listName.ValidationStatus.Failures.Count);
    }
}