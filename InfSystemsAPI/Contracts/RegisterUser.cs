namespace InfSystemsAPI.Contracts
{
    public record UserRegistration(string Username, string Password, int Role);
    public record UserValidation(string Username, string Password);
    public record UserRemoving(string Username);
}
