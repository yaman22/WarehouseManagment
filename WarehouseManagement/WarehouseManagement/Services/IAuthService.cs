using Microsoft.AspNetCore.Authentication;
using WarehouseManagement.Auth;

namespace WarehouseManagement.Services
{
    public interface IAuthService
    {
        Task<AuthenticationModel> RegisterAsync(RegisterModel model);

        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);

        Task<string> AddRoleToUserAsync(RoleModel model);

        Task<AuthenticationModel> RefreshTokenAsync(string token);

        Task<bool> RevokeTokenAsync(string token);
    }
}
