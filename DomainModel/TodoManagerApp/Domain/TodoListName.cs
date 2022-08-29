using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Validation;

namespace TodoManagerApp.Domain;

public class TodoListName : ValueOf<string, TodoListName>
{
    protected override void Validate()
    {
        if (string.IsNullOrEmpty(this.Value))
        {
            ValidationStatus.Append(Failure.For("ListName","Invalid list name"));
        }
    }
}