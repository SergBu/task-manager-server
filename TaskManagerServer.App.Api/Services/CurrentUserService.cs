using System.Security.Claims;
using TaskManagerServer.Lib.Core.Interfaces;

namespace TaskManagerServer.App.Api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public int UserId => -1;

    public List<string> GetRoles()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null)
            return new List<string>();

        return user.Identities
            .SelectMany(i => i.Claims.Where(c => c.Type == i.RoleClaimType).ToList())
            .Select(c => c.Value).ToList();
    }
}