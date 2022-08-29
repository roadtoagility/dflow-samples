using DFlow.Domain.BusinessObjects;

namespace TodoManagerApp.Domain;

public class TodoList: BaseEntity<TodoListId>
{
    private TodoListName ListName { get; }
    
    public TodoList(TodoListName listName, TodoListId identity, VersionId version) : base(identity, version)
    {
        ListName = listName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identity;
        yield return ListName;
    }
}