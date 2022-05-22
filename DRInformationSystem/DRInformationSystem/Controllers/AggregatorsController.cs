using System.IdentityModel.Tokens.Jwt;
using DRInformationSystem.Exceptions;
using DRInformationSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRInformationSystem.Controllers;

[ApiController]
[Route("aggregators")]
public class AggregatorsController : Controller
{
	private readonly IAggregatorsService _aggregatorsService;

	public AggregatorsController(IAggregatorsService aggregatorsService)
	{
		_aggregatorsService = aggregatorsService;
	}

	[HttpGet("/auth")]
	public async Task<IActionResult> AuthorizeAggregator([FromQuery] string login, [FromQuery] string password)
	{
		try
		{
			var token = await _aggregatorsService.AuthorizeAggregatorAsync(login, password);
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
			var response = new
			{
				access_token = encodedJwt,
				username = login
			};

			return Json(response);
		}
		catch (BadAuthException badAuthException)
		{
			return BadRequest(new { errorText = "Invalid username or password." });
		}
	}
}