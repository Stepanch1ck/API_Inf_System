namespace InfSystemsAPI.Contracts
{
    public record GetInfSystemRequests(string? Search, string? Attributes);
    public record CreateInfSystem(string Name, string Description, string[] AttributeKeys, string[] AttributeValues);
    public record DeleteInfSystem(string Name);
}
