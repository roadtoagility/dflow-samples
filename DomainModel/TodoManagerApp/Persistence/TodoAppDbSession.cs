using DFlow.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace TodoManagerApp.Persistence;

public class TodoAppDbSession<TRepository>:DbSession<TRepository>
{
    public TodoAppDbSession(DbContext context, TRepository repository) : base(context, repository)
    {
    }
}