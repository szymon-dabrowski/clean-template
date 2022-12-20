using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
internal class TestAggregateRootRepository : ITestAggregateRootRepository
{
    private readonly DbContext dbContext;

    public TestAggregateRootRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Add(TestAggregateRoot testAggregateRoot)
    {
        await dbContext
            .Set<TestAggregateRoot>()
            .AddAsync(testAggregateRoot);
    }

    public Task Delete(Guid testAggregateRootId)
    {
        var toDelete = dbContext
            .Set<TestAggregateRoot>()
            .Where(r => r.Id == testAggregateRootId);

        dbContext
            .Set<TestAggregateRoot>()
            .RemoveRange(toDelete);

        return Task.CompletedTask;
    }
}