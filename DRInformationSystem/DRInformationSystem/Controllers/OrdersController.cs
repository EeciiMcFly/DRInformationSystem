using AutoMapper;
using BusinessLogicLayer.Auth;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DRInformationSystem.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DRInformationSystem.Controllers;

[ApiController]
public class OrdersController : Controller
{
	private readonly IOrdersService _ordersService;
	private readonly IAggregatorsService _aggregatorsService;
	private readonly IConsumersService _consumersService;
	private readonly IMapper _mapper;

	public OrdersController(IOrdersService ordersService,
		IAggregatorsService aggregatorsService,
		IConsumersService consumersService,
		IMapper mapper)
	{
		_ordersService = ordersService;
		_aggregatorsService = aggregatorsService;
		_consumersService = consumersService;
		_mapper = mapper;
	}

	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[HttpGet("api/v1/aggregator/orders")]
	public async Task<IActionResult> GetAggregatorOrders()
	{
		var aggregatorLogin = User.Identity.Name;
		var aggregator = await _aggregatorsService.GetAggregatorByLoginAsync(aggregatorLogin);
		var orders = await _ordersService.GetOrdersForAggregatorAsync(aggregator.Id);
		var ordersDto = _mapper.Map<List<OrderDto>>(orders);

		return Ok(ordersDto);
	}

	[Authorize(Roles = AuthOptions.ConsumerRole)]
	[HttpGet("api/v1/consumer/orders")]
	public async Task<IActionResult> GetConsumerOrders()
	{
		var consumerLogin = User.Identity.Name;
		var consumer = await _consumersService.GetConsumersByLoginAsync(consumerLogin);
		var orders = await _ordersService.GetOrdersForConsumersAsync(consumer.Id);
		var ordersDto = _mapper.Map<List<OrderDto>>(orders);

		return Ok(ordersDto);
	}

	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[HttpPost("api/v1/aggregator/orders")]
	public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
	{
		var createOrderData = _mapper.Map<CreateOrderData>(createOrderDto);
		await _ordersService.CreateOrder(createOrderData);

		return Ok();
	}

	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[HttpPut("api/v1/aggregator/orders")]
	public async Task<IActionResult> CompleteOrder([FromBody] CompleteOrderDto completeOrderDto)
	{
		await _ordersService.CompleteOrder(completeOrderDto.OrderId, completeOrderDto.ResponseIds);

		return Ok();
	}

	[Authorize(Roles = AuthOptions.AggregatorRole)]
	[HttpDelete("api/v1/aggregator/orders")]
	public async Task<IActionResult> DeleteOrder([FromQuery] long orderId)
	{
		await _ordersService.DeleteOrder(orderId);

		return Ok();
	}
}