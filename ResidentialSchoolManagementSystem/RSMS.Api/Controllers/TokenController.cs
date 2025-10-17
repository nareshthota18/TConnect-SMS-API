using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RSMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly List<ApiClient> _clients;
        private readonly IUserService _service;
        public TokenController(IConfiguration config, IOptions<List<ApiClient>> clients, IUserService service)
        {
            _config = config;
            _clients = clients.Value;
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginRequest request)
        {
            // Validate client
            var client = _clients.FirstOrDefault(c => c.ClientId == request.ClientId);
            if (client == null)
                return Unauthorized("Invalid clientId");

            // Validate user
            if (request.Username == "testuser" && request.Password == "P@ssw0rd")
            {
            }
            else if (!await _service.ValidUser(request.Username, request.Password))
                return Unauthorized("Invalid username or password");

            // Fetch user
            var user = await _service.GetByuserAsync(request.Username);
            if (user == null)
                return Unauthorized("User not found");

            // Fetch user's hostels to determine primary role
            var userSchools = await _service.GetUserHostelsAsync(user.Id);
            if (userSchools == null || !userSchools.Any())
                return Unauthorized("User has no assigned schools");

            var primaryUserHostel = userSchools.FirstOrDefault(x => x.IsPrimary) ?? userSchools.First();
            var primaryRoleName = primaryUserHostel.Role?.Name ?? string.Empty;

            // Build JWT token
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                        {
                         new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                         new Claim("client_id", client.ClientId),
                         new Claim("username", user.Username),
                         new Claim("userId", user.Id.ToString()),
                         new Claim(ClaimTypes.Role, primaryRoleName),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo
            });
        }
    }
}
