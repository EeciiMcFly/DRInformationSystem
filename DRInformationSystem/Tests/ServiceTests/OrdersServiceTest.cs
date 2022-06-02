using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Moq;
using NUnit.Framework;

namespace Tests.ServiceTests;

[TestFixture]
public class OrdersServiceTest
{
	private OrdersService _ordersService;
	private Mock<IOrdersRepository> _ordersRepositoryMock;
	private Mock<IResponsesRepository> _responsesRepositoryMock;
	private Mock<IAggregatorsRepository> _aggregatorsRepositoryMock;

	[SetUp]
	public void SetUpMethod()
	{
		_ordersRepositoryMock = new Mock<IOrdersRepository>();
		_responsesRepositoryMock = new Mock<IResponsesRepository>();
		_aggregatorsRepositoryMock = new Mock<IAggregatorsRepository>();
		_ordersService = new OrdersService(_ordersRepositoryMock.Object,
			_responsesRepositoryMock.Object, _aggregatorsRepositoryMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_ordersRepositoryMock = null;
		_responsesRepositoryMock = null;
		_aggregatorsRepositoryMock = null;
		_ordersService = null;
	}

	[Test]
	public async Task GetOrdersForAggregatorAsync_WhenAggregatorIdIs5_UseSearchParamsWithId5()
	{
		var expectedAggregatorId = 5;
		OrderSearchParams actualSearchParams = null;
		_ordersRepositoryMock.Setup(x => x.GetAsync(It.IsAny<OrderSearchParams>()))
			.Callback((OrderSearchParams searchParams) => actualSearchParams = searchParams);

		await _ordersService.GetOrdersForAggregatorAsync(expectedAggregatorId);

		Assert.NotNull(actualSearchParams);
		Assert.IsNull(actualSearchParams.ConsumerId);
		Assert.AreEqual(expectedAggregatorId, actualSearchParams.AggregatorId);
	}

	[Test]
	public async Task GetOrdersForConsumerAsync_WhenConsumerIdIs5_UseSearchParamsWithId5()
	{
		var expectedConsumerId = 5;
		OrderSearchParams actualSearchParams = null;
		_ordersRepositoryMock.Setup(x => x.GetAsync(It.IsAny<OrderSearchParams>()))
			.Callback((OrderSearchParams searchParams) => actualSearchParams = searchParams);

		await _ordersService.GetOrdersForConsumersAsync(expectedConsumerId);

		Assert.NotNull(actualSearchParams);
		Assert.IsNull(actualSearchParams.AggregatorId);
		Assert.AreEqual(expectedConsumerId, actualSearchParams.ConsumerId);
	}

	[Test]
	public async Task CreateOrder_WhenAggregatorByIdNotExist_ThrowsNotExistedAggregatorException()
	{
		_aggregatorsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);

		var createOrderData = new CreateOrderData
		{
			AggregatorId = 0
		};

		Assert.ThrowsAsync<NotExistedAggregatorException>(async () =>
			await _ordersService.CreateOrder(createOrderData));
	}

	[Test]
	public async Task CreateOrder_WhenCreationDataCorrect_CreateCorrectOrder()
	{
		var expectedStartTime = new DateTime(2022, 06, 10, 0, 0, 0);
		var expectedEndTime = new DateTime(2022, 06, 12, 0, 0, 0);
		const OrderState expectedState = OrderState.NotFormatted;
		const int expectedAggregatorId = 0;
		var createOrderData = new CreateOrderData
		{
			PeriodStart = expectedStartTime,
			PeriodEnd = expectedEndTime,
			AggregatorId = expectedAggregatorId
		};

		var aggregator = new AggregatorModel();
		OrderModel actualOrderModel = null;
		_aggregatorsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(aggregator);

		_ordersRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<OrderModel>()))
			.Callback((OrderModel order) => actualOrderModel = order);

		await _ordersService.CreateOrder(createOrderData);

		Assert.NotNull(actualOrderModel);
		Assert.AreEqual(expectedStartTime, actualOrderModel.StartTimestamp);
		Assert.AreEqual(expectedEndTime, actualOrderModel.EndTimestamp);
		Assert.AreEqual(expectedState, actualOrderModel.State);
		Assert.AreEqual(expectedAggregatorId, actualOrderModel.AggregatorId);
	}

	[Test]
	public async Task CompleteOrder_WhenNoOrderWithId_ThrowNotExistedOrderException()
	{
		const int orderId = 0;
		var responseIds = new List<long>();
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);

		Assert.ThrowsAsync<NotExistedOrderException>(async () =>
			await _ordersService.CompleteOrder(orderId, responseIds));
	}

	[Test]
	public async Task CompleteOrder_WhenNoResponsesWithId_ThrowEmptyResponsesListForCompleteOrderException()
	{
		const int orderId = 0;
		var order = new OrderModel();
		var responseIds = new List<long>();
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(order);

		_responsesRepositoryMock.Setup(x => x.GetRangeByIdsAsync(It.IsAny<List<long>>()))
			.ReturnsAsync(new List<ResponseModel>());

		Assert.ThrowsAsync<EmptyResponsesListForCompleteOrderException>(async () =>
			await _ordersService.CompleteOrder(orderId, responseIds));
	}

	[Test]
	public async Task CompleteOrder_WhenAllRight_UpdateOrderWithCorrectValue()
	{
		const OrderState expectedState = OrderState.Formatted;
		var responseIds = new List<long>();
		var response = new ResponseModel();
		var expectedResponses = new List<ResponseModel> {response};
		const int orderId = 0;
		var order = new OrderModel();
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(order);

		_responsesRepositoryMock.Setup(x => x.GetRangeByIdsAsync(It.IsAny<List<long>>()))
			.ReturnsAsync(expectedResponses);

		OrderModel actualOrder = null;
		_ordersRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<OrderModel>()))
			.Callback((OrderModel order) => actualOrder = order);

		await _ordersService.CompleteOrder(orderId, responseIds);

		Assert.NotNull(actualOrder);
		Assert.AreEqual(expectedState, actualOrder.State);
		Assert.AreEqual(expectedResponses, actualOrder.Responses);
	}

	[Test]
	public async Task DeleteOrder_WhenNoOrderWithId_ThrowNotExistedOrderException()
	{
		const int orderId = 0;
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);

		Assert.ThrowsAsync<NotExistedOrderException>(async () =>
			await _ordersService.DeleteOrder(orderId));
	}

	[Test]
	public async Task DeleteOrder_WhenAllRight_DeleteCorrectOrder()
	{
		const int orderId = 0;
		var expectedOrder = new OrderModel
		{
			Id = orderId
		};

		OrderModel actualOrder = null;
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(expectedOrder);

		_ordersRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<OrderModel>()))
			.Callback((OrderModel order) => actualOrder = order);

		await _ordersService.DeleteOrder(orderId);

		Assert.NotNull(actualOrder);
		Assert.AreEqual(expectedOrder, actualOrder);
	}
}