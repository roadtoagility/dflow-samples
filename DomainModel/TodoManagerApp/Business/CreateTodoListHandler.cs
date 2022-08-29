using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;

namespace TodoManagerApp.Business;

public class CreateTodoListHandler:ICommandHandler<CreateTodoList,ExecutionResult>
{
    public CreateTodoListHandler()
    {
    }

    public Task<ExecutionResult> Execute(CreateTodoList command)
    {
        return Execute(command, new CancellationToken());
    }

    public Task<ExecutionResult> Execute(CreateTodoList command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ExecutionResult(command.IsValid,command.Failures));
    }
}