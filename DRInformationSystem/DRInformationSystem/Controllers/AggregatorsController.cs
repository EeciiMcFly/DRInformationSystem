using System.IdentityModel.Tokens.Jwt;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DRInformationSystem.Controllers;

[ApiController]
public class AggregatorsController : Controller
{
	private readonly IAggregatorsService _aggregatorsService;

	public AggregatorsController(IAggregatorsService aggregatorsService)
	{
		_aggregatorsService = aggregatorsService;
	}

	[HttpGet("api/v1/aggregator/auth")]
	[SwaggerResponse(200, "Successes authorize")]
	public async Task<IActionResult> AuthorizeAggregator([FromQuery] string login, [FromQuery] string password)
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
}