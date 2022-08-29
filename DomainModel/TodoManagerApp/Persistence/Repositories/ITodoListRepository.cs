using DFlow.Persistence.Repositories;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence.Repositories;

public interface ITodoListRepository:IRepository<TodoListState,TodoList>
{
}