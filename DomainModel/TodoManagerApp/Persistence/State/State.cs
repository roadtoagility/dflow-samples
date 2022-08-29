namespace TodoManagerApp.Persistence.State;

public abstract record State(Guid StateId, DateTime CreateAt, byte[] RowVersion);
