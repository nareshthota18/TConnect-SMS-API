using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
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
        private readonly IUserRepository _service;
        public TokenController(IConfiguration config, IOptions<List<ApiClient>> clients, IUserRepository service)
        {
            _config = config;
            _clients = clients.Value;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginRequest request)
        {
            // 1. Validate Client need to chnage db level
            var client = _clients.FirstOrDefault(c => c.ClientId == request.ClientId);
            if (client == null)
                return Unauthorized("Invalid clientId");

            // 2. Validate User (replace with your DB/Identity check)
            if (request.Username == "testuser" && request.Password == "P@ssw0rd")
            {
                //for testing
            }
            else
            {
                if (!_service.ValidUser(request.Username, request.Password).Result)
                    return Unauthorized("Invalid username or password");
            }

            // 3. Build Token
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim("client_id", client.ClientId),
            new Claim("username", request.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo
            });
        }
    }
}
