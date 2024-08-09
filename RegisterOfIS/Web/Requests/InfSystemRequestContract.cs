using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Requests
{
    public record GetInfSystemRequests(string? Search, string? Attributes);
    public record CreateInfSystem(string Name, string Description, List<string> AttributeKeys, List<string> AttributeValues);
    public record ChangeAttributes(string Name, List<string> AttrNames, List<string> AttrValues);
}
