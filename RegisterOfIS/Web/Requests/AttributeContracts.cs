
namespace RegisterOfIS.Requests
{
    public record CreateAttributeRequest(string Name, string Description, bool IsFixed, List<string> States);
    public record GetAttributes(string? Search);
    public record ChangeAttributeRequest(string Name, string Description, bool IsFixed);
    public record SearchAtrributesForm(string? Name, string? State);
    public record AttributeStateDto(string Name, string Value);
}
