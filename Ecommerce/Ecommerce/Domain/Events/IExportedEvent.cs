using System.Text.Json.Nodes;

namespace Ecommerce.Domain.Events;

public interface IExportedEvent
{
    Guid Id { get; }
    Guid AggregateId { get; }
    string AggregateType { get; }
    string EventType { get; }
    JsonNode EventData { get; }
}