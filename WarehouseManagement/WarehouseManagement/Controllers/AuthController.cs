using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WarehouseManagement.Auth;
using WarehouseManagement.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WarehouseManagement.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService ?? 
                throw new ArgumentNullException(nameof(_authService));
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result= await authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);

        }


        [HttpPost("LogIn")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            }
            return Ok(result);

        }


        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] RoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await authService.AddRoleToUserAsync(model);

            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            return Ok(model);

        }


        [HttpGet("GetRefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result=await authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result);
            }

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }


        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenDto dto)
        {
            var token = dto.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required");
            }

            var result=await authService.RevokeTokenAsync(token);

            if (!result)
            {
                return BadRequest("Token is Invalid");
            }

            return Ok();
        }

        private void SetRefreshTokenInCookie(string refreshToken,DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly=true,
                Expires=expires.ToLocalTime()
            };

            Response.Cookies.Append("refreshToken",refreshToken,cookieOptions);
        }
    }
}
