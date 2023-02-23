// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Persistence;
using Ecommerce.Business.Extensions;
using Ecommerce.Domain;
using Ecommerce.Domain.Aggregates;
using Ecommerce.Persistence.Repositories;

namespace Ecommerce.Business;

public sealed class ProductCreateHandler : ICommandHandler<ProductCreate, CommandResult>
{
    private readonly IDbSession<IProductRepository> _sessionDb;

    public ProductCreateHandler(IDbSession<IProductRepository> sessionDb)
    {
        this._sessionDb = sessionDb;
    }

    public Task<CommandResult> Execute(ProductCreate command)
    {
        return Execute(command, CancellationToken.None);
    }

    public async Task<CommandResult> Execute(ProductCreate command, CancellationToken cancellationToken)
    {
        var aggregate = ProductAggregationRoot.Create(ProductName.From(command.Name),
            ProductDescription.From(command.Description),
            ProductWeight.From(command.Weight));
        if (aggregate.IsValid)
        {
            await this._sessionDb.Repository.Add(aggregate.GetChange());
            await this._sessionDb.SaveChangesAsync(cancellationToken);
            return aggregate.GetChange().ToResultSucced();
        }

        return aggregate.GetChange().ToResultFailed();
    }
}