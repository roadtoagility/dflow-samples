// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Persistence;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence.Repositories;

namespace TodoManagerApp.Business;

public sealed class CreateTodoHandler:ICommandHandler<CreateTodo,CommandResult>
{
    private readonly IDbSession<ITodoListRepository> _sessionDb;
    public CreateTodoHandler(IDbSession<ITodoListRepository> sessionDb)
    {
        this._sessionDb = sessionDb;
    }

    public Task<CommandResult> Execute(CreateTodo command)
    {
        return Execute(command, CancellationToken.None);
    }

    public async Task<CommandResult> Execute(CreateTodo command, CancellationToken cancellationToken)
    {
        if (command.IsValid)
        {
            TodoList todolist = await this._sessionDb.Repository
                .GetById(command.ListId, cancellationToken);
            
            todolist.AddTodo(command.Description);

            if (todolist.IsValid)
            {
                await this._sessionDb.Repository.Add(todolist);
                await this._sessionDb.SaveChangesAsync(cancellationToken);
                
                return new CommandResult(todolist.IsValid, todolist.Identity.Value);
            }
            return new CommandResult(todolist.IsValid, todolist.Failures);    
        }
        return new CommandResult(command.IsValid, command.Failures);
    }
}