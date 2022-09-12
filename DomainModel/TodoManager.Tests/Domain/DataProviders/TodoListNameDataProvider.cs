// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using System.Collections.Generic;
using TodoManagerApp.Domain;

namespace TodoManager.Tests.Domain.DataProviders;

public class ValidListNameProvider : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { "My Todos", TodoListName.From("My Todos") }
        ,new object[] { "My", TodoListName.From("My") }
        ,new object[] { "", TodoListName.From("") }
    };

    public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class InvalidListNameProvider : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "", false, 1 }
    };

    public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}