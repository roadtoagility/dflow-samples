namespace Ecommerce.Capabilities.Persistence.State;

public sealed record ProductState(Guid Id, string Name, string Description
        , double Weight, byte[] RowVersion)
    : Ecommerce.Persistence.State.State(RowVersion);