
using System;
using DFlow.Domain.BusinessObjects;
using TodoManager.Tests.Data;
using TodoManagerApp.Domain;
using Xunit;

namespace TodoManager.Tests;

public class TodoListTests
{
    [Theory]
    [ClassData(typeof(TodoListProvider))]
    public void Create_Valid_TodoList(TodoListName listName, TodoListId identity, VersionId version, TodoList expected)
    {
        var list = new TodoList(listName,identity, version);
        Assert.Equal(expected,list);
    }
}