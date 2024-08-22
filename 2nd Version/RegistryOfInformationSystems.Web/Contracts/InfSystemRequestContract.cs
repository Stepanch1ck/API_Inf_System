

namespace RegistryOfInformationSystems.Contracts
{
    public record GetInfSystemRequests(string? Search, string? Attributes);
    public record CreateInfSystem(string Name, string Description, List<string> AttributeKeys, List<string> AttributeValues);
    public record ChangeInfSystem(string Name,string newName, string newDescription, List<string> AttrNames, List<string> AttrValues);
    public record GetInfSystemWithAttribute(string AttrName);
}
