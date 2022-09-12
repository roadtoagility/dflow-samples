// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.


using DFlow.Business.Cqrs.CommandHandlers;
using Microsoft.AspNetCore.Mvc;
using TodoManagerApp.Business;

namespace TodoManagerWebAPI.ApiEndpoints;

public static class TodoApiEndpoints
{
    public static void StateChangeApis(WebApplication app)
    {
        app.MapPost("/api/lists", async ([FromBody]CreateTodoList todoList,ICommandHandler<CreateTodoList, CommandResult> handler) =>
        {
            if (todoList.IsValid == false)
            {
                return Results.BadRequest(todoList.Failures);
            }
            var result = await handler.Execute(todoList);

            if (result.IsSucceed == false)
            {
                return Results.BadRequest(todoList.Failures);
            }
                    
            return Results.Ok(result);
        });

        app.MapPut("/api/lists/{todoListId}/todos",
            async (CreateTodo todo, ICommandHandler<CreateTodo, CommandResult> handler) =>
            {
                if (todo.IsValid == false)
                {
                    Results.BadRequest(todo.Failures);
                }

                var result = await handler.Execute(todo);

                if (result.IsSucceed == false)
                {
                    Results.BadRequest(todo.Failures);
                }
                    
                return Results.Ok(result);
            });
    }
}