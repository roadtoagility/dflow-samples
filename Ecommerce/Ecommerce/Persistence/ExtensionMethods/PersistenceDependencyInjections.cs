// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Persistence;
using Ecommerce.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Persistence.ExtensionMethods
{
    public static class PersistenceDependencyInjections
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDbSession<IProductRepository>, DbSession<IProductRepository>>();
        }
    }
}