// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Linq.Expressions;
using Ecommerce.Domain;
using Ecommerce.Domain.Aggregates;
using Ecommerce.Framework.Persistence.Repositories;
using Ecommerce.Persistence.State;

namespace Ecommerce.Persistence.Repositories;

public interface IProductRepository : IRepository<ProductAggregationRoot, ProductState, Product>
{
    Task<Product> GetById(ProductId id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> FindAsync(Expression<Func<ProductState, bool>> predicate, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}