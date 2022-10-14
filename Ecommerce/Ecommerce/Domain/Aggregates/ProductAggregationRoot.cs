using Ecommerce.Domain.Events;
using Ecommerce.Framework.Domain;

namespace Ecommerce.Domain.Aggregates;

public sealed class ProductAggregationRoot : AggregationRootBase<Product, ProductId>
{
    public ProductAggregationRoot(Product product)
    {
        if (product.IsValid)
        {
            Apply(product);

            if (product.IsNew())
            {
                Raise(ProductUpdatedEvent.For(product));
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

    public static ProductAggregationRoot Create(Product product)
    {
        return new ProductAggregationRoot(product);
    }
    
    public void Update(ProductDescription description, ProductWeight weight)
    {
        var updated = Product.CombineDescriptionAndWeight(AggregateRootEntity, description, weight);
        
        if (updated.IsValid)
        {
            Apply(updated);
            Raise(ProductUpdatedEvent.For(updated));
        }
        
        AppendValidationResult(updated.Failures);
    }
}