// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.


using System.Collections.Immutable;
using System.Linq.Expressions;
using DFlow.Domain.BusinessObjects;
using Ecommerce.Domain;
using Ecommerce.Domain.Aggregates;
using Ecommerce.Persistence.ExtensionMethods;
using Ecommerce.Persistence.State;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly int recordPageSizeLimit = 20;
    private readonly int initialPageNumber = 1;

    private readonly EcommerceAppDbContext _dbContext;

    public ProductRepository(EcommerceAppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task Add(ProductAggregationRoot aggregate)
    {
        await Add(aggregate.GetChange());
        var outbox = aggregate.ToOutBox();
        await this._dbContext
            .Set<AggregateState>()
            .AddRangeAsync(outbox);
    }

    public async Task Add(Product entity)
    {
        var entry = entity.ToProductState();

        var cancel = new CancellationTokenSource();

        var oldState = await GetById(entity.Identity, cancel.Token)
            .ConfigureAwait(false);

        if (oldState.Equals(Product.Empty()))
        {
            this._dbContext.Set<ProductState>().Add(entry);
        }
        else
        {
            if (VersionId.Next(oldState.Version) > entity.Version)
            {
                throw new DbUpdateConcurrencyException("This version is not the most updated for this object.");
            }

            this._dbContext.Entry(oldState).CurrentValues.SetValues(entry);
        }
    }

    public async Task Remove(Product entity)
    {
        var cancel = new CancellationTokenSource();

        var oldState = await GetById(entity.Identity, cancel.Token)
            .ConfigureAwait(false);

        if (oldState.Equals(Product.Empty()))
        {
            throw new ArgumentException(
                $"O produto {entity.Name} com identificação {entity.Identity} não foi encontrado.");
        }

        var entry = entity.ToProductState();
        this._dbContext.Set<ProductState>().Remove(entry);
    }

    public async Task<IReadOnlyList<Product>> FindAsync(Expression<Func<ProductState, bool>> predicate
        , CancellationToken cancellationToken)
    {
        return await FindAsync(predicate, this.initialPageNumber, this.recordPageSizeLimit, cancellationToken);
    }

    public async Task<Product> GetById(ProductId id, CancellationToken cancellation)
    {
        var result = await FindAsync(p => p.Id.Equals(id), cancellation);

        if (result.Count == 0)
        {
            return Product.Empty();
        }

        return result.First();
    }

    public async Task<IReadOnlyList<Product>> FindAsync(Expression<Func<ProductState, bool>> predicate,
        int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            return await this._dbContext.Set<ProductState>()
                .Where(predicate).AsNoTracking()
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(t => t.ToProduct())
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        catch (InvalidOperationException)
        {
            return ImmutableList<Product>.Empty;
        }
    }
}