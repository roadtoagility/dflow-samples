// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Domain.Validation;
using DFlow.Persistence;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence.Repositories;

namespace TodoManagerApp.Business;

public sealed class CreateTodoListHandler:ICommandHandler<CreateTodoList,CommandResult>
{
    private readonly IDbSession<ITodoListRepository> _sessionDb;
    public CreateTodoListHandler(IDbSession<ITodoListRepository> sessionDb)
    {
        this._sessionDb = sessionDb;
    }

    public Task<CommandResult> Execute(CreateTodoList command)
    {
        return Execute(command, CancellationToken.None);
    }

    public async Task<CommandResult> Execute(CreateTodoList command, CancellationToken cancellationToken)
    {
        if (command.IsValid)
        {
            var list = TodoList.EmptyTodoList(TodoListName.From(command.ListName), TodoListId.From(1));

            if (list.IsValid)
            {
                await this._sessionDb.Repository.Add(list);
                await this._sessionDb.SaveChangesAsync(cancellationToken);
                
                return new CommandResult(command.IsValid, list.Identity.Value);
            }
            
            return new CommandResult(list.IsValid, list.Failures);    
        }
        
        return new CommandResult(command.IsValid, command.Failures);
    }
}