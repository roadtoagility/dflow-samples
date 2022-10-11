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

        var cancel = new CancellationTokenSource();

        var oldState = await FindAsync(filter => 
                filter.Id.Equals(entity.Identity.Value), cancel.Token)
            .ConfigureAwait(false);

        // if (oldState[0] == false))
        // {
            this._dbContext.Set<TodoListState>().Add(entry);
        // }
        // else
        // {
        //     if (VersionId.Next(oldState.Version) > entity.Version)
        //     {
        //         throw new DbUpdateConcurrencyException("This version is not the most updated for this object.");
        //     }
        //
        //     DbContext.Entry(oldState).CurrentValues.SetValues(entry);
        // }
    }
    
    public Task Remove(TodoList entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<IReadOnlyList<TodoList>> FindAsync(Expression<Func<TodoListState, bool>> predicate, CancellationToken cancellationToken)
    {
        return await this._dbContext.Set<TodoListState>()
            .Where(predicate).AsNoTracking()
            .Select(t => t.ToTodoList())
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<TodoList> GetById(TodoListId id, CancellationToken cancellation)
    {
        return await FindAsync(id => id.Id.Equals(id), cancellation)
            .ContinueWith(result => result.Result.First(),cancellation);
    }
}