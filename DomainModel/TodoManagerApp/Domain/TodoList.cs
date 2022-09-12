// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Validation;
using DFlow.Persistence;

namespace TodoManagerApp.Domain;

public class TodoList: BaseEntity<TodoListId>
{
    private IList<Todo> _todos;

    public TodoList(TodoListName listName, TodoListId identity, ImmutableList<Todo> todos, VersionId version) 
        : base(identity, version)
    {
        ListName = listName;
        _todos = new List<Todo>(todos);
    }

    public static TodoList EmptyTodoList(TodoListName name, TodoListId listId)
    {
        return new TodoList(name, listId, ImmutableList<Todo>.Empty, VersionId.New());
    }
    
    public TodoListName ListName { get; }

    public IReadOnlyList<Todo> Todos
    {
        get
        {
            return this._todos.ToImmutableList();
        }
    }
    public TodoList AddTodo(TodoDescription description)
    {
        var todo = Todo.New(description, TodoId.From(this._todos.Count + 1));
        
        if (todo.IsValid)
        {
            this._todos.Add(todo);
        }
        else
        {
            AppendValidationResult(Failure.For("Todo","O todo precisa ser valido"));
        }

        return this;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identity;
        yield return ListName;
        yield return Version;
    }
}