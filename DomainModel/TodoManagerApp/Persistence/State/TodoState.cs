namespace TodoManagerApp.Persistence.State;

public sealed record TodoState(string Description, bool IsDone, Guid StateId, DateTime CreateAt, byte[] RowVersion)
    : State(StateId, CreateAt, RowVersion);

