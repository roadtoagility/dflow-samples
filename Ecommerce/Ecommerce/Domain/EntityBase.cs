// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using DFlow.Domain.BusinessObjects;
using Ecommerce.Framework.Domain.Events;

namespace Ecommerce.Domain;

public interface IDomainEvents
{
    IReadOnlyList<DomainEvent> GetEvents();
}

public abstract class EntityBase<TEntityId> : BaseEntity<TEntityId>, IDomainEvents
{
    private IList<DomainEvent> _events = new List<DomainEvent>();
    
    protected EntityBase(TEntityId identity, VersionId version) : base(identity, version)
    {
    }

    public void RaisedEvent(DomainEvent @event)
    {
        this._events.Add(@event);
    }
    
    public IReadOnlyList<DomainEvent> GetEvents()
    {
        return this._events.ToImmutableList();
    }


}