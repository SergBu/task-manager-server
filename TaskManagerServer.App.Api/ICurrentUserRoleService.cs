namespace TaskManagerServer.App.Api;

public interface ICurrentUserRoleService
{
    List<string> GetRoles();

    public bool IsInAnyOfRoles(IEnumerable<string> roles);
}