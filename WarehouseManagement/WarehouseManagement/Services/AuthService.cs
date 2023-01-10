using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WarehouseManagement.Auth;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly JWT jwt;

        public AuthService(UserManager<ApplicationUser> _userManager, IMapper _mapper,IOptions<JWT> _jwt, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager ??
                throw new ArgumentNullException(nameof(_userManager));

            mapper = _mapper ??
                throw new ArgumentNullException(nameof(_mapper));

            jwt = _jwt.Value ??
                throw new ArgumentNullException(nameof(_jwt));

            roleManager = _roleManager ??
                throw new ArgumentNullException(nameof(_roleManager));
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthenticationModel();

            var user = await userManager.FindByEmailAsync(model.Email);

            if(user is null || !await userManager.CheckPasswordAsync(user,model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName= user.UserName;
            //authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);

                authModel.RefreshToken = activeRefreshToken.Token;

                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();

                authModel.RefreshToken = refreshToken.Token;

                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

                user.RefreshTokens.Add(refreshToken);

                await userManager.UpdateAsync(user);
            }


            return authModel;
        }

        public async Task<AuthenticationModel> RegisterAsync(RegisterModel model)
        {
            if (await userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthenticationModel { Message = "Email is already registered!" };
            }

            if (await userManager.FindByNameAsync(model.UserName) is not null)
            {
                return new AuthenticationModel { Message = "UserName is already registered!" };
            }

            var user = mapper.Map<ApplicationUser>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }

                return new AuthenticationModel { Message = errors };
            }

            await userManager.AddToRoleAsync(user, UserRoles.User);

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthenticationModel
            {
                Email = user.Email,
                //ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
        }

        public async Task<string> AddRoleToUserAsync(RoleModel model)
        {
            var user = await userManager.FindByIdAsync(model.userId);

            if(user is null || !await roleManager.RoleExistsAsync(model.Role))
            {
                return "Invalid user ID or Role";
            }

            if(await userManager.IsInRoleAsync(user,model.Role))
            {
                return "User is already assigned to this role";
            }

            var result = await userManager.AddToRoleAsync(user, model.Role);

            if(result.Succeeded)
            {
                return string.Empty;
            }

            return "Something went wrong!";

        }

        public async Task<AuthenticationModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthenticationModel();

            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                authModel.Message = "Invalid Token";

                return authModel;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive Token";

                return authModel;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();

            user.RefreshTokens.Add(newRefreshToken);

            await userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);

            authModel.IsAuthenticated = true;
            authModel.Token= new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user.Email;
            authModel.UserName=user.UserName;

            var roles = await userManager.GetRolesAsync(user);

            authModel.Roles=roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return authModel;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                return false;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            await userManager.UpdateAsync(user);

            return true;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token=Convert.ToBase64String(randomNumber),
                ExpiresOn=DateTime.UtcNow.AddDays(10),
                CreatedOn=DateTime.UtcNow,
            }; 
        }
    }
}
