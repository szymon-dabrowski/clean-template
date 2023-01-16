using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Clean.Modules.Shared.Infrastructure.ExecutionContext;
public class ExecutionContextAccessor : IExecutionContextAccessor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => GetUserIdFromHttpContext();

    private Guid GetUserIdFromHttpContext()
    {
        var sub = httpContextAccessor.HttpContext
            ?.User
            ?.FindFirst(JwtRegisteredClaimNames.Sub);

        return Guid.TryParse(sub?.Value, out var userId)
            ? userId
            : throw new ApplicationException("User context is not available");
    }
}