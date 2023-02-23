// // Copyright (C) 2023  Road to Agility

using DFlow.BusinessObjects;

namespace DomainModel.SimpleApp.Domain;

public class Product: EntityBase<ProductName>
{
    public Product(ProductName identity, Description description, 
        Weight weight, VersionId version) 
        : base(identity, version)
    {
        Weight = weight;
        Description = description;
    }

    public Weight Weight { get; }
    public Description Description { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identity;
        yield return Weight;
    }
}