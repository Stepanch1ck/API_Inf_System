using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystem.WebRequests.RequestContracts
{
    public record UserRegistration(string Username, string Password, int Role);
    public record UserValidation(string Username, string Password);
    public record UserRemoving(string Username);
    public record GetUserByUsername(string Username);
    public record GetUserByRole(string Role);
    public record ChangePawword(string Login, string Password);
}
