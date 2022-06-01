using AutoMapper;
using BusinessLogicLayer.Auth;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DRInformationSystem.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DRInformationSystem.Controllers;

[ApiController]
public class SheddingsController : Controller
{
	private readonly ISheddingsService _sheddingsService;
	private readonly IConsumersService _consumersService;
	private readonly IMapper _mapper;

	public SheddingsController(ISheddingsService sheddingsService,
		IConsumersService consumersService,
		IMapper mapper)
	{
		_sheddingsService = sheddingsService;
		_consumersService = consumersService;
		_mapper = mapper;
	}

	[HttpGet("api/v1/aggregators/sheddings")]
	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[SwaggerResponse(200, "List of sheddings for aggregator user", typeof(SheddingDto))]
	public async Task<IActionResult> GetAggregatorSheddings([FromQuery] long orderId)
	{
		var sheddings = await _sheddingsService.GetSheddingForAggregatorOrder(orderId);
		var sheddingDtos = _mapper.Map<List<SheddingDto>>(sheddings);

		return Ok(sheddingDtos);
	}

	[HttpGet("api/v1/consumers/sheddings")]
	[Authorize(Roles = AuthOptions.ConsumerRole)]
	[SwaggerResponse(200, "List of sheddings for consumer user", typeof(SheddingDto))]
	public async Task<IActionResult> GetConsumerShedings([FromQuery] long orderId)
	{
		var consumerLogin = User.Identity.Name;
		var consumer = await _consumersService.GetConsumersByLoginAsync(consumerLogin);
		var sheddings = await _sheddingsService.GetSheddingForConsumerOrder(orderId, consumer.Id);
		var sheddingDtos = _mapper.Map<List<SheddingDto>>(sheddings);

		return Ok(sheddingDtos);
	}

	[HttpPost("api/v1/sheddings")]
	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[SwaggerResponse(200, "Successes creation")]
	public async Task<IActionResult> CreateShedding([FromBody] CreateSheddingDto createSheddingDto)
	{
		var createSheddingData = _mapper.Map<CreateSheddingData>(createSheddingDto);
		await _sheddingsService.CreateShedding(createSheddingData);

		return Ok();
	}

	[HttpPut("api/v1/sheddings")]
	[Authorize]
	public async Task<IActionResult> UpdateShedding([FromBody] UpdateSheddingDto updateSheddingDto)
	{
		await _sheddingsService.SetNewStatusForShedding(updateSheddingDto.Id, updateSheddingDto.Status);

		return Ok();
	}
	
	[HttpDelete("api/v1/sheddings")]
	[Authorize(Roles = AuthOptions.AggregatorRole)]
	public async Task<IActionResult> DeleteShedding([FromQuery] long sheddingId)
	{
		await _sheddingsService.DeleteShedding(sheddingId);

		return Ok();
	}
}