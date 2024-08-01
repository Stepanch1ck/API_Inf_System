using API_Inf_System.DBContext;

namespace API_Inf_System.IServices
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
