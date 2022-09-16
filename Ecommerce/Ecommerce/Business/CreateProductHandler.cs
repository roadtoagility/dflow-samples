// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Persistence;
using Ecommerce.Business.Extensions;
using Ecommerce.Domain;
using Ecommerce.Persistence.Repositories;

namespace Ecommerce.Business;

public sealed class CreateProductHandler:ICommandHandler<CreateProduct,CommandResult>
{
    private readonly IDbSession<IProductRepository> _sessionDb;
    public CreateProductHandler(IDbSession<IProductRepository> sessionDb)
    {
        this._sessionDb = sessionDb;
    }

    public Task<CommandResult> Execute(CreateProduct command)
    {
        return Execute(command, CancellationToken.None);
    }

    public async Task<CommandResult> Execute(CreateProduct command, CancellationToken cancellationToken)
    {
        var product = Product.NewProduct(command.Name, command.Description, command.Weight);

        if (product.IsValid)
        {
            await this._sessionDb.Repository.Add(product);
            await this._sessionDb.SaveChangesAsync(cancellationToken);
            
            return product.ToResultSucced();
        }
        return product.ToResultFailed();    
    }
}