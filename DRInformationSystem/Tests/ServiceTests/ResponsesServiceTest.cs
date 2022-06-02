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
public class ResponsesServiceTest
{
	private ResponsesService _responsesService;
	private Mock<IResponsesRepository> _responsesRepositoryMock;
	private Mock<IOrdersRepository> _ordersRepositoryMock;
	private Mock<IConsumersRepository> _consumersRepositoryMock;

	[SetUp]
	public void SetUpMethod()
	{
		_responsesRepositoryMock = new Mock<IResponsesRepository>();
		_ordersRepositoryMock = new Mock<IOrdersRepository>();
		_consumersRepositoryMock = new Mock<IConsumersRepository>();
		_responsesService = new ResponsesService(_responsesRepositoryMock.Object,
			_ordersRepositoryMock.Object, _consumersRepositoryMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_responsesRepositoryMock = null;
		_ordersRepositoryMock = null;
		_consumersRepositoryMock = null;
		_responsesService = null;
	}

	[Test]
	public async Task GetResponsesForOrderAsync_WhenOrderIdIs5_UseSearchParamsWithId5()
	{
		var expectedOrderId = 5;
		ResponseSearchParams actualSearchParams = null;
		_responsesRepositoryMock.Setup(x => x.GetAsync(It.IsAny<ResponseSearchParams>()))
			.Callback((ResponseSearchParams searchParams) => actualSearchParams = searchParams);

		await _responsesService.GetResponsesForOrderAsync(expectedOrderId);

		Assert.NotNull(actualSearchParams);
		Assert.IsNull(actualSearchParams.ConsumerId);
		Assert.AreEqual(expectedOrderId, actualSearchParams.OrderId);
	}

	[Test]
	public async Task GetResponsesForOrderAsync_WhenConsumerIdIs5_UseSearchParamsWithId5()
	{
		var expectedConsumerId = 5;
		ResponseSearchParams actualSearchParams = null;
		_responsesRepositoryMock.Setup(x => x.GetAsync(It.IsAny<ResponseSearchParams>()))
			.Callback((ResponseSearchParams searchParams) => actualSearchParams = searchParams);

		await _responsesService.GetResponsesForConsumerAsync(expectedConsumerId);

		Assert.NotNull(actualSearchParams);
		Assert.IsNull(actualSearchParams.OrderId);
		Assert.AreEqual(expectedConsumerId, actualSearchParams.ConsumerId);
	}

	[Test]
	public async Task CreateResponseAsync_WhenOrderByIdNotExist_ThrowNotExistedOrderException()
	{
		var createResponseData = new CreateResponseData
		{
			OrderId = 0
		};
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);
		
		Assert.ThrowsAsync<NotExistedOrderException>(async () =>
			await _responsesService.CreateResponseAsync(createResponseData));
	}
	
	[Test]
	public async Task CreateResponseAsync_WhenConsumerByIdNotExist_ThrowNotExistedConsumerException()
	{
		var order = new OrderModel();
		var createResponseData = new CreateResponseData
		{
			ConsumerId = 0
		};
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(order);
		_consumersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);
		
		Assert.ThrowsAsync<NotExistedConsumerException>(async () =>
			await _responsesService.CreateResponseAsync(createResponseData));
	}
	
	[Test]
	public async Task CreateResponseAsync_WhenCreationDataCorrect_CreateCorrectResponse()
	{
		const int expectedOrderId = 5;
		const int expectedConsumerId = 4;
		var expectedReduceData = new List<long>
			{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
		var order = new OrderModel();
		var consumer = new ConsumerModel();
		var createResponseData = new CreateResponseData
		{
			OrderId = expectedOrderId,
			ConsumerId = expectedConsumerId,
			ReduceData = expectedReduceData
		};

		ResponseModel actualResponse = null;
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(order);
		_consumersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(consumer);
		_responsesRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<ResponseModel>()))
			.Callback((ResponseModel response) => actualResponse = response);

		await _responsesService.CreateResponseAsync(createResponseData);
		
		Assert.NotNull(actualResponse);
		Assert.AreEqual(expectedOrderId, actualResponse.OrderId);
		Assert.AreEqual(expectedConsumerId, actualResponse.ConsumerId);
		Assert.AreEqual(expectedReduceData, actualResponse.ReduceData);
	}

	[Test]
	public async Task DeleteResponseAsync_WhenResponseByIdNotExist_ThrowNotExistedResponseException()
	{
		_responsesRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);
		
		Assert.ThrowsAsync<NotExistedResponseException>(async () =>
			await _responsesService.DeleteResponseAsync(5));
	}
	
	[Test]
	public async Task DeleteResponseAsync_WhenResponseByIdExist_DeleteResponse()
	{
		var expectedResponse = new ResponseModel
		{
			Id = 5,
		};
		ResponseModel actualResponse = null;
		_responsesRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(expectedResponse);
		_responsesRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ResponseModel>()))
			.Callback((ResponseModel response) => actualResponse = response);

		await _responsesService.DeleteResponseAsync(5);
		
		Assert.NotNull(actualResponse);
		Assert.AreEqual(expectedResponse, actualResponse);
	}
}