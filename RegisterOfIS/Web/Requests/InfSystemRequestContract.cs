

namespace RegisterOfIS.Requests
{
    public record ChangeInfSystemRequest(string Name, string Description);
    public record CreateInfSystemRequest(string Name, string Description, Dictionary<string, string> attributes);
    public record SearchInfSystemsForm(string? Name, string? Attr);
}
