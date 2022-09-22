namespace Ecommerce.Persistence.State;

public sealed record AggregateState(Guid Id
    , Guid AggregateId
    , string AggregationName
    , string EventType
    , string EventData);