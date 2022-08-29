using DFlow.Domain.Command;
using TodoManagerApp.Domain;

namespace TodoManagerApp.Business;

public class CreateTodoList: BaseCommand
{
    public TodoListName ListName { get; }
    public CreateTodoList(string listName)
    {
        ListName = TodoListName.From(listName);
    }
}