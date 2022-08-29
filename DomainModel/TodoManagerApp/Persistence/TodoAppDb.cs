using Microsoft.EntityFrameworkCore;
using TodoManagerApp.Persistence.Mappings;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence;

public sealed class TodoAppDb: DbContext
{
    public TodoAppDb(DbContextOptions<TodoAppDb> contextOptions)
        : base(contextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // new TodoListStateMapping().Configure(modelBuilder.Entity<TodoListState>());
        // new TodoStateMapping().Configure(modelBuilder.Entity<TodoState>());
    }
}