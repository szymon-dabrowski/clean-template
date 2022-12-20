using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;

namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public class UnitOfWorkCommandHandlerDecorator<TCommand, TResult>
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly IRequestHandler<TCommand, TResult> decorated;
    private readonly IUnitOfWork unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(
        IRequestHandler<TCommand, TResult> decorated,
        IUnitOfWork unitOfWork)
    {
        this.decorated = decorated;
        this.unitOfWork = unitOfWork;
    }

    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var result = await decorated.Handle(request, cancellationToken);

        await unitOfWork.Commit(cancellationToken);

        return result;
    }
}