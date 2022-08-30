using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Validation;

namespace TodoManagerApp.Domain;

public class TodoList: BaseEntity<TodoListId>
{
    private IList<Todo> _todos;
    public TodoListName ListName { get; }
    
    public IReadOnlyList<Todo> Todos { get; }
    
    public TodoList(TodoListName listName, TodoListId identity, VersionId version) : base(identity, version)
    {
        ListName = listName;
        _todos = new List<Todo>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identity;
        yield return ListName;
    }

    public TodoList AddTodo(Todo todo)
    {
        if (todo.IsValid)
        {
            this._todos.Add(todo);
        }
        else
        {
            AppendValidationResult(Failure.For("Todo","O todo precisa ser valido"));
        }

        return this;
    }
}