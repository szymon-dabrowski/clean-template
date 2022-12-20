namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken = default);
}