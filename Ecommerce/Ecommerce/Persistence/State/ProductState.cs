namespace Ecommerce.Persistence.State;

public sealed record ProductState(Guid ProductStateId, string Name, string Description
        , float Weight, byte[] RowVersion)
    : State(RowVersion);

