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
        
        AppendValidationResult(weight.ValidationStatus.Failures);
        AppendValidationResult(description.ValidationStatus.Failures);
        AppendValidationResult(identity.ValidationStatus.Failures);
    }

    public Weight Weight { get; }
    public Description Description { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identity;
        yield return Weight;
    }
    
    public static Product From(ProductName name, Weight weight, 
        Description descr, VersionId versionId)
    {
        return new Product(name,descr,weight,versionId);
    }
    public static Product New(string name, double weight, string descr)
    {
        return From(ProductName.From(name),
            Weight.From(weight),
            Description.From(descr),
            VersionId.New());
    }

    public static Product NameUpdate(Product product,
        ProductName newName)
    {
        return From(newName,
            product.Weight,
            product.Description,
            VersionId.Next(product.Version));
    }
}