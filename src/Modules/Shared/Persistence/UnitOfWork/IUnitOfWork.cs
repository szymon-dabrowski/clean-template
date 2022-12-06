namespace Clean.Modules.Shared.Persistence;
public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}