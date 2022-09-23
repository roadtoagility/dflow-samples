using System.Collections.Immutable;
using DFlow.Domain.Aggregates;
using Ecommerce.Domain.Events;

namespace Ecommerce.Domain.Aggregates;

public sealed class ProductAggregationRoot : ObjectBasedAggregationRoot<Product, ProductId>
{
    private readonly IList<DomainEvent> _changes = new List<DomainEvent>();

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

    private void Raise(DomainEvent @event)
    {
        this._changes.Add(@event);
    }

    public IReadOnlyList<DomainEvent> GetEvents()
    {
        return this._changes.ToImmutableList();
    }

    public static ProductAggregationRoot Create(ProductName name, ProductDescription description, ProductWeight weight)
    {
        var product = Product.NewProduct(name, description, weight);
        return new ProductAggregationRoot(product);
    }
}