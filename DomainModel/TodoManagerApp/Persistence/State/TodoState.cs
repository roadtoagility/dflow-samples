namespace TodoManagerApp.Persistence.State;

public sealed record TodoState(int TodoStateId, string Description, bool IsDone, byte[] RowVersion)
    : State(RowVersion);

