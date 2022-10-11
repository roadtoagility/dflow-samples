// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using FluentMediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoManagerApp.Business;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence;

Console.WriteLine("== Simple Cqrs App to Create a User");
            
var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<TodoAppDbContext>(options =>
    options.UseSqlite("Data Source=tododb_dev.sqlite;"));

serviceCollection.AddMediatorOperations();
serviceCollection.AddRepositories();
serviceCollection.AddCommandHandlers();

var provider = serviceCollection.BuildServiceProvider();
var mediator = provider.GetService<IMediator>();

var result = mediator?.SendAsync<CommandResult>(new CreateTodoList {ListName = "Compromissos"})
    .GetAwaiter()
    .GetResult();
                
Console.WriteLine("press any key to exit.");
Console.ReadKey();