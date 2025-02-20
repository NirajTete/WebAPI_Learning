using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_Learning.Models;

namespace WebAPI_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Admin")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please enter username and password");

            if (model.UserName != "admin" || model.Password != "admin")
                return BadRequest("Invalid username or password");

            var token = GenerateJwtToken(model.UserName);

            return Ok(new LoginResponseDTO { UserName = model.UserName, Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            var secretKey = _configuration.GetValue<string>("JWTSecret");
            var issuer = _configuration.GetValue<string>("Issuer");
            var audience = _configuration.GetValue<string>("Audience");

            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 16)
                throw new Exception("JWTSecret must be at least 16 characters long");

            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin"),
        }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}

