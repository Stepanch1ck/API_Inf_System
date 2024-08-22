namespace RegistryOfInformationSystems.Contracts
{
    public record CreateAttributeRequest(string Name, string Description, List<string> States);
    public record GetAttributes(string? Search);
    public record DeleteAttribute(string Name);
    public record AddNewStateRequest(string Name, string Value);
    public record RemoveStateRequest(string Name, string Value);
    public record ChangeAttributeRequest(string Name, string NewName, string Description);
}
