
using System.Collections.Immutable;
using System.Text.Json.Nodes;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Domain.Events.DomainEvents;
using Ecommerce.Business;
using Ecommerce.Domain.Events;

namespace Ecommerce.Domain.Aggregates;

public sealed class ProductAggregationRoot : ObjectBasedAggregationRoot<Product, ProductId>
{
    private readonly IList<DomainEvent> _changes = new List<DomainEvent>();

    private void Raise(DomainEvent @event) => this._changes.Add(@event);

    public IReadOnlyList<DomainEvent> GetEvents() => this._changes.ToImmutableList();
    
    public ProductAggregationRoot(Product product)
    {
        if (product.IsValid)
        {
            Apply(product);

            if (product.IsNew())
            {
                Raise(ProductCreatedEvent.For(product));
            }
        }
        else
        {
            AppendValidationResult(product.Failures);
        }
    }

    public static ProductAggregationRoot Create(ProductName name, ProductDescription description, ProductWeight weight)
    {
        var product = Product.NewProduct(name, description, weight);
        return new ProductAggregationRoot(product);
    }
}
