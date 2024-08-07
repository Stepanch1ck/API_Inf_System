using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystem.WebRequests.RequestContracts
{
    public record CreateAttributeRequest(string Name, string Description, List<string> States);
    public record GetAttributes(string? Search);
    public record DeleteAttribute(string Name);
}
