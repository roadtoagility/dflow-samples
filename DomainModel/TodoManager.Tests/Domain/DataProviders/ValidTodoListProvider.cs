// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using DFlow.Domain.BusinessObjects;
using TodoManagerApp.Domain;

namespace TodoManager.Tests.Domain.DataProviders;

public class ValidTodoListProvider : IEnumerable<object[]>
{
    static Todo todo = new Todo(
        TodoDescription.From("escrever testes"),
        TodoStatus.NotDone, 
        TodoId.From(1),
        VersionId.New());

    static readonly TodoList expected = new(TodoListName.From("My Todos"),TodoListId.From(1),
        new List<Todo>()
        {
            Todo.From(TodoDescription.From("writing tests"),TodoStatus.NotDone, TodoId.From(1),VersionId.New())
        }.ToImmutableList(),
        VersionId.New());
    
    private readonly List<object[]> _data = new()
    {
        new object[] { TodoListName.From("My Todos"), TodoListId.From(1), new List<Todo>(){todo},VersionId.New(), expected}
    };

    public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}