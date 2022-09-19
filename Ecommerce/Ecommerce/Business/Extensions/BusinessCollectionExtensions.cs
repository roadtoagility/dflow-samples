// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.


using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Business.Cqrs.QueryHandlers;
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
                builder.On<ProductCreate>().PipelineAsync()
                    .Return<CommandResult, ProductCreateHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<ProductList>().PipelineAsync()
                    .Return<Result<ProductView>, ProductListHandler>(
                        async (handler, request) => await handler.Execute(request));
            });
        }

        public static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<ProductCreate, CommandResult>, ProductCreateHandler>();
            services.AddScoped<IQueryHandler<ProductList, Result<ProductView>>, ProductListHandler>();
        }
    }
}