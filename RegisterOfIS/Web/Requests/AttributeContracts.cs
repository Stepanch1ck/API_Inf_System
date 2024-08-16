using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Requests
{
    public record CreateAttributeRequest(string Name, string Description, List<string> States);
    public record GetAttributes(string? Search);
    public record DeleteAttribute(string Name);
    public record AddNewStateRequest(string Name, string Value);
    public record RemoveStateRequest(string Name, string Value);
    public record ChangeAttributeRequest(string Name, string NewName, string Description);
}
