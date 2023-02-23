﻿// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections;
using System.Collections.Generic;
using DFlow.Domain.BusinessObjects;
using Ecommerce.Domain;

namespace Ecommerce.Tests.Domain.DataProviders;

public class ProductValidInputProvider : IEnumerable<object[]>
{
    static ProductId productId = ProductId.From(Guid.NewGuid());
    private static readonly Product product = Product.From(
        productId,
        ProductName.From("name"),
        ProductDescription.From("descrição"),
        ProductWeight.From(1.0f),
        VersionId.From(1)
    );

    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            productId, ProductName.From("name"), ProductDescription.From("descrição"),
            ProductWeight.From(1.0f), VersionId.From(1), product
        }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        return this._data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}