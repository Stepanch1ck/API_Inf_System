namespace RegistryOfInformationSystems.Contracts
{
    public record UserRegistration(string Username, string Password, string Role);
    public record ChangeUser(string Login, string Password, string Role);
    public record GetAllUsersRequest();
}
