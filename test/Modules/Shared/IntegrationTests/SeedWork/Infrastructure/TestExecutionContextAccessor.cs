using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
internal class TestExecutionContextAccessor : IExecutionContextAccessor
{
    public Guid UserId { get; set; }
}