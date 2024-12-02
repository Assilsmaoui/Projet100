using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TP7.DTO;
using TP7.Models;

namespace TP7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(RegisterUSerDTO registerUSer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByNameAsync(registerUSer.Username);
            if (user != null)
            {
                return BadRequest("Utilisateur existant");
            }
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = registerUSer.Username,
                Email = registerUSer.Email,
            };
           var result =await userManager.CreateAsync(applicationUser,registerUSer.Password);
            if(result.Succeeded)
            {
                return Created();
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                return BadRequest("Wrong Credentials");
            }
            if(await userManager.CheckPasswordAsync(user,login.Password))
            {
                var claims = new List<Claim>();
                //claims.Add(new Claim("name", "value")); 
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti,
Guid.NewGuid().ToString()));
                //signingCredentials 
                var key = new
SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: sc
                    );
                var _token = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = login.Username,
                };
                return Ok(_token);
            }
            return BadRequest("Wrong Credentials");
        }
    }
}
