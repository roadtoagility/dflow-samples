namespace TodoManagerApp.Persistence.State;

public sealed record TodoListState(IList<TodoState> Todos, Guid StateId, DateTime CreateAt, byte[] RowVersion)
    : State(StateId, CreateAt, RowVersion);
