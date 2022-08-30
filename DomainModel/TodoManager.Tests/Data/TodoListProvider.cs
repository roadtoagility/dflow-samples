using System;
using System.Collections;
using System.Collections.Generic;
using DFlow.Domain.BusinessObjects;
using TodoManagerApp.Domain;

namespace TodoManager.Tests.Data;

public class TodoListProvider : IEnumerable<object[]>
{
    static readonly TodoList expected  = new TodoList(TodoListName.From("My Todos"), 
        TodoListId.From(1), VersionId.New());
    
    private readonly List<object[]> _data = new()
    {
        new object[] { TodoListName.From("My Todos"), TodoListId.From(1), VersionId.New(), expected}
    };

    public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}