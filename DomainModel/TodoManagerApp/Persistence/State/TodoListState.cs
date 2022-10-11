namespace TodoManagerApp.Persistence.State;

public sealed record TodoListState
{
    public string Name { get; set; }
    
    public int Id { get; set; }
    public ICollection<TodoState> Todos { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] RowVersion { get; set; }
}
