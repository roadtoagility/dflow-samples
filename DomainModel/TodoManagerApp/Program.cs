// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Persistence;
using FluentMediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoManagerApp;
using TodoManagerApp.Business;
using TodoManagerApp.Persistence;
using TodoManagerApp.Persistence.Repositories;

Console.WriteLine("== Simple Cqrs App to Create a User");
            
var serviceCollection = new ServiceCollection();
serviceCollection.AddFluentMediator(builder =>
{
    builder.On<CreateTodoList>().CancellablePipelineAsync()
        .Return<ExecutionResult, CreateTodoListHandler>(
            async(handler, request, token) => await handler.Execute(request, token));
});
serviceCollection.AddDbContext<TodoAppDb>(options =>
    options.UseSqlite("Data Source=tododb_dev.sqlite;"));

serviceCollection.AddScoped<ITodoListRepository, TodoListRepository>();
serviceCollection.AddScoped<IDbSession<ITodoListRepository>, TodoAppDbSession<ITodoListRepository>>();
serviceCollection.AddScoped<CreateTodoListHandler>();
            
var provider = serviceCollection.BuildServiceProvider();
var mediator = provider.GetService<IMediator>();

var result = mediator?.SendAsync<CommandResult<Guid>>(new CreateTodoList("Compromissos"))
    .GetAwaiter()
    .GetResult();
                
Console.WriteLine("press any key to exit.");
Console.ReadKey();