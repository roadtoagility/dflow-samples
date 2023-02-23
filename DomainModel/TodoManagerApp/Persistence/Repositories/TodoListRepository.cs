using System.Linq.Expressions;
using DFlow.Domain.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence.ExtensionMethods;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence.Repositories;

public class TodoListRepository: ITodoListRepository
{
    private readonly TodoAppDbContext _dbContext;
    public TodoListRepository(TodoAppDbContext dbContext)
    {
        this._dbContext = dbContext;

    }
    public async Task Add(TodoList entity)
    {
        var entry = entity.ToTodoListState();
         this._dbContext.Set<TodoListState>().Add(entry);

    }

    public async Task<IReadOnlyList<TodoList>> FindAsync(Expression<Func<TodoListState, bool>> predicate
        , CancellationToken cancellationToken)
    {
        return await this._dbContext.Set<TodoListState>()
            .Where(predicate).AsNoTracking()
                .Select(t => t.ToTodoList())
                    .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}