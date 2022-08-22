//using Community_BackEnd.Data;
//using Community_BackEnd.Entities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Community_BackEnd.Controllers
//{
//    public class LoginController : Controller
//    {
//        private IConfiguration _config;

//        public LoginController(IConfiguration config)
//        {
//            _config = config;
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        public IActionResult Login([FromBody] Login userLogin)
//        {
//            var user = Authenticate(userLogin);

//            if (user != null)
//            {
//                var token = Generate(user);
//                return Ok(token);
//            }

//            return NotFound("User not found");
//        }

//        private string Generate(User user)
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.UserName),
//                new Claim(ClaimTypes.Name, user.DisplayName),
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Surname, user.Surname),
//                new Claim(ClaimTypes.Role, user.IdentityUserRoles.First().RoleId)
//            };

//            var token = new JwtSecurityToken(_config["Jwt:ValidIssuer"],
//              _config["Jwt:ValidAudience"],
//              claims,
//              expires: DateTime.Now.AddMinutes(15),
//              signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        private User Authenticate(Login userLogin)
//        {
//            var currentUser = UserConstants._appUsers.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

//            if (currentUser != null)
//            {
//                return currentUser;
//            }

//            return null;
//        }
//    }
//}
