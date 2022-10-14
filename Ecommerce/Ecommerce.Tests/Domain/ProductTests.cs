// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.BusinessObjects;
using Ecommerce.Domain;
using Ecommerce.Tests.Domain.DataProviders;
using Xunit;

namespace Ecommerce.Tests.Domain;

public class ProductTests
{
    [Theory]
    [ClassData(typeof(ProductValidInputProvider))]
    public void CreateValidProduct(
        ProductId productId,
        ProductName name,
        ProductDescription description,
        ProductWeight weight,
        VersionId versionId, Product expected)
    {
        var product = Product.From(productId, name, description, weight, versionId);
        Assert.Equal(expected, product);
    }
}