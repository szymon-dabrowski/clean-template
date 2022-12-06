namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}