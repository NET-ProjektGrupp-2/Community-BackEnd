using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Community_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Community_BackEnd.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private IUserService _userService;
	//private readonly JWTSetting setting;
	private readonly IConfiguration _configuration;
	private readonly AppDbContext context;
	public UserController(IUserService userService)
	{
		//context = AppDbContext;
		_userService = userService;
	}

	// GET: api/<UserController>
	[HttpGet]
	public IEnumerable<string> Get()
	{
		return new string[] { "value1", "value2" };
	}

	// GET api/<UserController>/5
	[HttpGet("{id}")]
	public string Get(int id)
	{
		return "value";
	}
    [Route("[Authenticate]")]
    [AllowAnonymous]
	[HttpPost("authenticate")]
	public IActionResult Authenticate([FromBody] AuthenticateModel model)
	{
		var user = _userService.Authenticate(model.Username, model.Password);

		if (user == null)
			return BadRequest(new { message = "Username or password is incorrect" });

		return Ok(user);
	}
	
}
