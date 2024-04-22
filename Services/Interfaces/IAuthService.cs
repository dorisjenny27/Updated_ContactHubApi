using ContactHub.Model.DTOs;
using ContactHub.Model.Entity;
using System.Security.Claims;

namespace ContactHub.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJWT(User user, List<string> roles, List<Claim> claims);

        Task<LoginResult> Login(string email, string password);

        Task<Dictionary<string, string>> ValidateLoggedInUser(ClaimsPrincipal user, string userId);

        Task<User> CreateUser(UserToAddDTO model);

    }
}
