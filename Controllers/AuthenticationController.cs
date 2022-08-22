using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Community_BackEnd.Entities;
using Community_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
//using IdentityServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Community_BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{



    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;

    // authentication set up
    private IUserService _userService;
    public AuthenticationController(
         UserManager<IdentityUser> userManager,
         RoleManager<IdentityRole> roleManager,
         IConfiguration configuration,
         IUserService userService,
         AppDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _userService = userService;
        _userService = userService;
        _dbContext = dbContext;
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        //var resetToken=await _userManager.GeneratePasswordResetTokenAsync(user);
        //await _userManager.ResetPasswordAsync(user, resetToken, model.Password);
        await _userManager.AddPasswordAsync(user, model.Password);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
    //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    //{
    //    using (var hmac = new HMACSHA512())
    //    {
    //        passwordSalt = hmac.Key;
    //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    //    }
    //}
    //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    //{
    //    using (var hmac = new HMACSHA512(passwordSalt))
    //    {
    //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    //        return computedHash.SequenceEqual(passwordHash);
    //    }
    //}
    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };

        return refreshToken;
    }

    //[HttpPost("refresh-token")]
    //public async Task<ActionResult<string>> RefreshToken()
    //{
    //    var refreshToken = Request.Cookies["refreshToken"];

    //    if (!user.RefreshToken.Equals(refreshToken))
    //    {
    //        return Unauthorized("Invalid Refresh Token.");
    //    }
    //    else if (AppUser.TokenExpires < DateTime.Now)
    //    {
    //        return Unauthorized("Token expired.");
    //    }

    //    string token = CreateToken(user);
    //    var newRefreshToken = GenerateRefreshToken();
    //    SetRefreshToken(newRefreshToken);

    //    return Ok(token);
    //}
    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] Registration model)
    {

        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        IdentityUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] Registration model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        IdentityUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Authenticate([FromBody] AuthenticateModel model)
    {

        var user = _userService.Authenticate(model.Username, model.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }

    [Authorize(Roles = Role.Admin)]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        // only allow admins to access other user records
        var currentUserId = int.Parse(User.Identity.Name);
        if (id != currentUserId && !User.IsInRole(Role.Admin))
            return Forbid();

        var user = _userService.GetById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
    [HttpGet]
    public IActionResult AdminsEndpoint()
    {
        var currentUser = GetCurrentUser();

        return Ok($"Hi {currentUser.Firstname}, you are an {currentUser.IdentityUserRoles}");
    }


    [HttpGet("Authors")]
    [Authorize(Roles = "Author")]
    public IActionResult SellersEndpoint()
    {
        var currentUser = GetCurrentUser();

        return Ok($"Hi {currentUser.Firstname}, you are a {currentUser.IdentityUserRoles}");
    }

    [HttpGet("AdminsAndAuthor")]
    [Authorize(Roles = "Administrator,Author")]
    public IActionResult AdminsAndAuthorsEndpoint()
    {
        var currentUser = GetCurrentUser();

        return Ok($"Hi {currentUser.Firstname}, you are an {currentUser.IdentityUserRoles}");
    }

    [HttpGet("Public")]
    public IActionResult Public()
    {
        return Ok("Hi, you're on public property");
    }

    private User GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new User("user")
            {
                UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                //  EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                Firstname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value
              //  IdentityUserRoles = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            };
        }

        return null;
    }
}
