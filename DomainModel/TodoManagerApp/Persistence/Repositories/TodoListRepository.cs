using System.Linq.Expressions;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence.Repositories;

public class TodoListRepository: ITodoListRepository
{
    public Task Add(TodoList entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TodoList>> FindAsync(Expression<Func<TodoListState, bool>> predicate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Remove(TodoList entity)
    {
        throw new NotImplementedException();
    }
}