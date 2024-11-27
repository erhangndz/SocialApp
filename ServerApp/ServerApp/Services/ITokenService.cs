using ServerApp.Models;

namespace ServerApp.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateJwtToken(AppUser user);
    }
}
