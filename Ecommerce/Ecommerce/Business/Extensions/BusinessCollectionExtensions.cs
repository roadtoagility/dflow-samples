// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.


using DFlow.Business.Cqrs.CommandHandlers;
using FluentMediator;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Business.Extensions
{
    public static class BusinessCollectionExtensions
    {
        public static void AddMediatorOperations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddFluentMediator(builder =>
            {
                builder.On<CreateProduct>().PipelineAsync()
                    .Return<CommandResult, CreateProductHandler>(
                        async(handler, request) => await handler.Execute(request));
            });
        }
        public static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateProduct,CommandResult>, CreateProductHandler>();
        }
    }
}