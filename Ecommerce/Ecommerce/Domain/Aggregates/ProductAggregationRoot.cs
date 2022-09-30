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