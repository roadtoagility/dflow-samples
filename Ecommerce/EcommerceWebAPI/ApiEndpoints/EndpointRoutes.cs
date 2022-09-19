// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Business.Cqrs.QueryHandlers;
using Ecommerce.Business;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebAPI.ApiEndpoints;

public static class EndpointRoutes
{
    public static void StateChangeApis(WebApplication app)
    {
        app.MapPost("/api/v1/products", async ([FromBody] ProductCreate command
            , ICommandHandler<ProductCreate, CommandResult> handler) =>
        {
            var result = await handler.Execute(command);

            if (result.IsSucceed == false)
            {
                return Results.BadRequest(result.Violations);
            }

            return Results.Ok(result);
        });

        app.MapGet("/api/v1/products", async (IQueryHandler<ProductList, Result<ProductView>> handler) =>
        {
            var result = await handler.Execute(new ProductList("", "", 1, 10));
            return Results.Ok(result);
        });
    }
}