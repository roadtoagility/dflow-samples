// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Persistence;
using Ecommerce.Business.Extensions;
using Ecommerce.Domain;
using Ecommerce.Domain.Aggregates;
using Ecommerce.Persistence.Repositories;

namespace Ecommerce.Business;

public sealed class ProductUpdateHandler : ICommandHandler<ProductUpdate, CommandResult>
{
    private readonly IDbSession<IProductRepository> _sessionDb;

    public ProductUpdateHandler(IDbSession<IProductRepository> sessionDb)
    {
        this._sessionDb = sessionDb;
    }

    public Task<CommandResult> Execute(ProductUpdate command)
    {
        return Execute(command, CancellationToken.None);
    }

    public async Task<CommandResult> Execute(ProductUpdate command, CancellationToken cancellationToken)
    {
        var product = await this._sessionDb.Repository.GetById(ProductId.From(command.Id),cancellationToken);
        
        var aggregate = ProductAggregationRoot.Create(product);
        
        aggregate.Update(ProductDescription.From(command.Description),ProductWeight.From(command.Weight));

        if (!aggregate.IsValid)
        {
            return aggregate.GetChange().ToResultFailed();
        }

        await this._sessionDb.Repository.Add(aggregate.GetChange());
        await this._sessionDb.SaveChangesAsync(cancellationToken);
        return aggregate.GetChange().ToResultSucced();

    }
}