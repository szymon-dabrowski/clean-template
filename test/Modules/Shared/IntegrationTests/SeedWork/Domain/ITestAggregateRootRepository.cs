namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
internal interface ITestAggregateRootRepository
{
    Task Add(TestAggregateRoot testAggregateRoot);

    Task Delete(Guid testAggregateRootId);
}