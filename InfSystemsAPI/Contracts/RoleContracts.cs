namespace InfSystemsAPI.Contracts
{
    public record RoleCreatingRequest(string Name, string Description, int Power);
    public record RoleDeletingRequest(string Name);
}
