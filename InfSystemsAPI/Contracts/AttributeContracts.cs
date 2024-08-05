namespace InfSystemsAPI.Contracts
{
    public record CreateAttributeRequest(string Name, string Description, string[] States);
    public record GetAttributes(string? Search);
    public record DeleteAttribute(string Name);
}
