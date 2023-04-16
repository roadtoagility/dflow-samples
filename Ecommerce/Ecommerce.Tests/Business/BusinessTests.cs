// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Persistence;
using Ecommerce.Business;
using Ecommerce.Capabilities.Persistence.Repositories;
using Ecommerce.Tests.Domain.DataProviders;
using Xunit;

namespace Ecommerce.Tests.Business;

public class BusinessTests
{
    [Theory]
    [ClassData(typeof(ProductInputProvider))]
    public async void CreateProduct(
        string description,
        string name,
        float weight,
        bool expected)
    {
        var command = new ProductCreate(description, name, weight);
        var session = NSubstitute.Substitute.For<IDbSession<IProductRepository>>();
        
        var handler = new ProductCreateHandler(session);
        var result = await handler.Execute(command);
            
        Assert.Equal(expected, result.IsSucceded);
    }
}