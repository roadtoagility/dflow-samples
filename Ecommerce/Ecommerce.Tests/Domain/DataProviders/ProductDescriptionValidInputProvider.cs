﻿// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using System.Collections.Generic;

namespace Ecommerce.Tests.Domain.DataProviders;

public class ProductDescriptionValidInputProvider : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { "Descrição do produto cotando a história dele.", true}
    };

    public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}