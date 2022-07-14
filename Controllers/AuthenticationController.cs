using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Community_BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{

    // GET: api/<AuthenticationController>
    [HttpGet]
	public IEnumerable<string> Get()
	{
		return new string[] { "value1", "value2" };
	}

	// GET api/<AuthenticationController>/5
	[HttpGet("{id}")]
	public string Get(int id)
	{
		return "value";
	}

	// POST api/<AuthenticationController>
	[HttpPost]
	public void Post([FromBody] string value)
	{
	}

	// PUT api/<AuthenticationController>/5
	[HttpPut("{id}")]
	public void Put(int id, [FromBody] string value)
	{
	}

	// DELETE api/<AuthenticationController>/5
	[HttpDelete("{id}")]
	public void Delete(int id)
	{
	}
}
