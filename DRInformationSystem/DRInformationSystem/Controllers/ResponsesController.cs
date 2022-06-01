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
public class ResponsesController : Controller
{
	private readonly IResponsesService _responsesService;
	private readonly IMapper _mapper;

	public ResponsesController(IResponsesService responsesService,
		IMapper mapper)
	{
		_responsesService = responsesService;
		_mapper = mapper;
	}

	[HttpGet("api/v1/responses")]
	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[SwaggerResponse(200, "List of responses for order", typeof(ResponseDto))]
	public async Task<IActionResult> GetResponses([FromQuery] long orderId)
	{
		var responses = await _responsesService.GetResponsesForOrderAsync(orderId);
		var responseDtos = _mapper.Map<List<ResponseDto>>(responses);

		return Ok(responseDtos);
	}

	[HttpPost("api/v1/responses")]
	[Authorize(Roles = AuthOptions.ConsumerRole)]
	[SwaggerResponse(200, "Success creation")]
	public async Task<IActionResult> CreateResponse([FromBody] CreateResponseDto createResponseDto)
	{
		var createResponseData = _mapper.Map<CreateResponseData>(createResponseDto);
		await _responsesService.CreateResponseAsync(createResponseData);

		return Ok();
	}

	[HttpDelete("api/v1/responses")]
	[Authorize(Roles = AuthOptions.ConsumerRole)]
	[SwaggerResponse(200, "Success deletion")]
	public async Task<IActionResult> DeleteResponse([FromQuery] long responseId)
	{
		await _responsesService.DeleteResponseAsync(responseId);

		return Ok();
	}
}