// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.Command;
using TodoManagerApp.Domain;

namespace TodoManagerApp.Business;

public class CreateTodo: BaseCommand
{
    public TodoDescription Description { get; }
    public TodoListId ListId { get; }
    
    public CreateTodo(string description, int todoList)
    {
        Description = TodoDescription.From(description);
        ListId = TodoListId.From(todoList);
    }
}