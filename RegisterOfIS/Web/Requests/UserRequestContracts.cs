using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Requests
{
    public record UserRegistration(string Username, string Password, string Role);
    public record ChangeUser(string Login, string Password, string Role);
    public record GetAllUsersRequest();
}
