using System.Collections.Immutable;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using Ecommerce.Framework.Domain.Events;

namespace Ecommerce.Framework.Domain;

public abstract class AggregationRootBase<TRoot, TEntityId> : 
    ObjectBasedAggregationRoot<TRoot, TEntityId> where TRoot:BaseEntity<TEntityId>
{
    private readonly IList<DomainEvent> _changes = new List<DomainEvent>();

    protected void Raise(DomainEvent @event)
    {
        this._changes.Add(@event);
    }

    public IReadOnlyList<DomainEvent> GetEvents()
    {
        return this._changes.ToImmutableList();
    }
}