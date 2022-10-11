namespace TodoManagerApp.Persistence.State;

public record State(byte[] RowVersion)
{
    public bool IsDeleted { get; set; }
}
