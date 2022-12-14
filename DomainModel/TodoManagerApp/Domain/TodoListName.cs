// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Validation;

namespace TodoManagerApp.Domain;

public class TodoListName : ValueOf<string, TodoListName>
{
    protected override void Validate()
    {
        if (string.IsNullOrEmpty(this.Value))
        {
            ValidationStatus.Append(Failure.For("ListName","Invalid list name"));
        }
    }
}