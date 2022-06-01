using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DRInformationSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DRInformationSystem.Controllers;

[ApiController]
public class ConsumersController : Controller
{
	private readonly IConsumersService _consumersService;
	private readonly IMapper _mapper;

	public ConsumersController(IConsumersService consumersService,
		IMapper mapper)
	{
		_consumersService = consumersService;
		_mapper = mapper;
	}

	[HttpGet("api/v1/consumers/auth")]
	[SwaggerResponse(200, "Successes authorize")]
	public async Task<IActionResult> AuthorizeConsumer([FromQuery] string login, [FromQuery] string password)
	{
		var token = await _consumersService.AuthorizeConsumerAsync(login, password);
		var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
		var response = new
		{
			access_token = encodedJwt,
			username = login
		};

		return Json(response);
	}

	[HttpPost("api/v1/consumers/register")]
	[SwaggerResponse(200, "Successes registration")]
	[SwaggerResponse(400, "Incorrect registration data")]
	public async Task<IActionResult> RegisterConsumer([FromBody] RegistrationConsumerDto registrationConsumerDto)
	{
		var registerData = _mapper.Map<RegisterConsumerData>(registrationConsumerDto);
		await _consumersService.RegisterConsumerAsync(registerData);

		return Ok();
	}

	[HttpGet("api/v1/consumers")]
	[SwaggerResponse(200, "List of consumers", typeof(ConsumerDto))]
	public async Task<IActionResult> GetConsumers()
	{
		var consumers = await _consumersService.GetConsumersAsync();
		var consumerDtos = _mapper.Map<List<ConsumerDto>>(consumers);

		return Ok(consumerDtos);
	}
}