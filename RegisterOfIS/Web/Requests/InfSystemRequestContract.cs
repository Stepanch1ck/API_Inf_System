

namespace RegisterOfIS.Requests
{
    public record GetInfSystemRequests(string? Search, string? Attributes);
    public record CreateInfSystem(string Name, string Description, List<string> AttributeKeys, List<string> AttributeValues);
    public record ChangeAttributes(string Name, List<string> AttrNames, List<string> AttrValues);
    public record GetInfSystemWithAttribute(string AttrName);
}
