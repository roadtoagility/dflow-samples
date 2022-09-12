// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using DFlow.Domain.BusinessObjects;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence.ExtensionMethods;

public static class BusinessObjectsExtensions
{
    public static TodoState ToTodoState(this Todo todo)
        => new TodoState(
            todo.Identity.Value,
            todo.Description.Value,
            todo.IsDone.Value,
            BitConverter.GetBytes(todo.Version.Value));

    public static TodoListState ToTodoListState(this TodoList list)
        => new TodoListState
        {
            Name = list.ListName.Value,
            Id = list.Identity.Value,
            Todos = list.Todos.Select(t => t.ToTodoState()).ToList(),
            RowVersion = BitConverter.GetBytes(list.Version.Value)
        };

    public static Todo ToTodo(this TodoState state)
        => Todo.From(
            TodoDescription.From(state.Description),
            TodoStatus.From(state.IsDone),
            TodoId.From(state.TodoStateId),
            VersionId.From(BitConverter.ToInt32(state.RowVersion)));
    
    public static TodoList ToTodoList(this TodoListState state)
        => new TodoList(
            TodoListName.From(state.Name),
            TodoListId.From(state.Id),
            state.Todos.Select(t => t.ToTodo()).ToImmutableList(),
            VersionId.From(BitConverter.ToInt32(state.RowVersion)));
}