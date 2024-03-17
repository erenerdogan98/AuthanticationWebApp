using AuthanticationWebAPI.Models;

namespace AuthanticationWebAPI.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(UserLogin user);
        Task<bool> Login(UserLogin user);
        Task<bool> Register(UserLogin user);
    }
}
