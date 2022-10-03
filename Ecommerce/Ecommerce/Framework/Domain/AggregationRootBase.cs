// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.Aggregates;
using Ecommerce.Domain;
using Ecommerce.Framework.Domain.Events;

namespace Ecommerce.Framework.Domain;

public abstract class AggregationRootBase<TRoot, TEntityId> : 
    ObjectBasedAggregationRoot<TRoot, TEntityId> where TRoot:EntityBase<TEntityId>
{
    protected void Raise(DomainEvent @event)
    {
        AggregateRootEntity.RaisedEvent(@event);
    }
}