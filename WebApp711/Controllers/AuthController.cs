using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Claims;
using WebApp711.DB;
using WebApp711.DTO;

namespace WebApp711.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ItCompany1135Context db;

        public AuthController(ItCompany1135Context db)
        {
            this.db = db;
        }

        [HttpPost]
        public ActionResult Login(AuthDTO login)
        {
            var client = db.Clients.FirstOrDefault(s => s.Login == login.Login
                && s.Password == login.Password);

            if (client == null)
                return Forbid();

            var claims = new List<Claim> {
                new Claim(ClaimValueTypes.Sid, client.Sid),
            };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(token);
        }
    }
}


