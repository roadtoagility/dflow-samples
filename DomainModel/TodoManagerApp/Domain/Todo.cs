using DFlow.Domain.BusinessObjects;

namespace TodoManagerApp.Domain;

public class Todo: BaseEntity<TodoId>
{
    private TodoDescription Description { get; }
    
    public Todo(TodoDescription description, TodoId identity, VersionId version) : base(identity, version)
    {
        Description = description;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identity;
        yield return Description;
    }

    void AddTodo(Todo todo)
    {
        
    }
}