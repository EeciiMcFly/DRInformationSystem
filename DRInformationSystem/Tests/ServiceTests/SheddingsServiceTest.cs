using System;
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
public class SheddingsServiceTest
{
	private SheddingsService _sheddingsService;
	private Mock<ISheddingsRepository> _sheddingsRepositoryMock;
	private Mock<IOrdersRepository> _ordersRepositoryMock;
	private Mock<IConsumersRepository> _consumersRepositoryMock;

	[SetUp]
	public void SetUpMethod()
	{
		_sheddingsRepositoryMock = new Mock<ISheddingsRepository>();
		_ordersRepositoryMock = new Mock<IOrdersRepository>();
		_consumersRepositoryMock = new Mock<IConsumersRepository>();
		_sheddingsService = new SheddingsService(_sheddingsRepositoryMock.Object,
			_ordersRepositoryMock.Object, _consumersRepositoryMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_sheddingsRepositoryMock = null;
		_ordersRepositoryMock = null;
		_consumersRepositoryMock = null;
		_sheddingsService = null;
	}

	[Test]
	public async Task GetSheddingForAggregatorOrder_WhenOrderIdIs5_UseSearchParamsWithId5()
	{
		var expectedOrderId = 5;
		SheddingSearchParams actualSearchParams = null;
		_sheddingsRepositoryMock.Setup(x => x.GetAsync(It.IsAny<SheddingSearchParams>()))
			.Callback((SheddingSearchParams searchParams) => actualSearchParams = searchParams);

		await _sheddingsService.GetSheddingForAggregatorOrder(expectedOrderId);

		Assert.NotNull(actualSearchParams);
		Assert.IsNull(actualSearchParams.ConsumerId);
		Assert.AreEqual(expectedOrderId, actualSearchParams.OrderId);
	}

	[Test]
	public async Task GetSheddingForConsumerOrder_WhenOrderIdIs5AndConsumerIdIs4_UseSearchParamsWithId5AndId4()
	{
		var expectedOrderId = 5;
		var expectedConsumerId = 4;
		SheddingSearchParams actualSearchParams = null;
		_sheddingsRepositoryMock.Setup(x => x.GetAsync(It.IsAny<SheddingSearchParams>()))
			.Callback((SheddingSearchParams searchParams) => actualSearchParams = searchParams);

		await _sheddingsService.GetSheddingForConsumerOrder(expectedOrderId, expectedConsumerId);

		Assert.NotNull(actualSearchParams);
		Assert.AreEqual(expectedConsumerId, actualSearchParams.ConsumerId);
		Assert.AreEqual(expectedOrderId, actualSearchParams.OrderId);
	}

	[Test]
	public async Task CreateShedding_WhenOrderByIdNotExist_ThrowNotExistedOrderException()
	{
		var createSheddingData = new CreateSheddingData
		{
			OrderId = 5
		};

		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);

		Assert.ThrowsAsync<NotExistedOrderException>(async () =>
			await _sheddingsService.CreateShedding(createSheddingData));
	}

	[Test]
	public async Task CreateShedding_WhenConsumerByIdNotExist_ThrowNotExistedConsumerException()
	{
		var order = new OrderModel();
		var createSheddingData = new CreateSheddingData
		{
			OrderId = 5,
			ConsumerId = 4
		};

		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(order);

		_consumersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);

		Assert.ThrowsAsync<NotExistedConsumerException>(async () =>
			await _sheddingsService.CreateShedding(createSheddingData));
	}


	[Test]
	public async Task CreateShedding_WhenCreationDataCorrect_CreateCorrectShedding()
	{
		var expectedStartTime = new DateTime(2022, 06, 10, 0, 0, 0);
		const int expectedDuration = 2;
		const int expectedVolume = 5;
		const int expectedOrderId = 4;
		const SheddingState expectedStatus = SheddingState.Prepared;
		const int expectedConsumerId = 6;
		var createSheddingData = new CreateSheddingData
		{
			StartTime = expectedStartTime,
			Duration = expectedDuration,
			Volume = expectedVolume,
			OrderId = expectedOrderId,
			ConsumerId = expectedConsumerId
		};

		SheddingModel actualShedding = null;
		_ordersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(new OrderModel());

		_consumersRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(new ConsumerModel());

		_sheddingsRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<SheddingModel>()))
			.Callback((SheddingModel shedding) => actualShedding = shedding);

		await _sheddingsService.CreateShedding(createSheddingData);

		Assert.NotNull(actualShedding);
		Assert.AreEqual(expectedStartTime, actualShedding.StartTimestamp);
		Assert.AreEqual(expectedDuration, actualShedding.Duration);
		Assert.AreEqual(expectedVolume, actualShedding.Volume);
		Assert.AreEqual(expectedOrderId, actualShedding.OrderId);
		Assert.AreEqual(expectedStatus, actualShedding.Status);
		Assert.AreEqual(expectedConsumerId, actualShedding.ConsumerId);
	}

	[Test]
	public async Task SetNewStatusForShedding_WhenSheddingByIdNotExist_ThrowNotExistedSheddingException()
	{
		const int sheddingId = 5;
		_sheddingsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);

		Assert.ThrowsAsync<NotExistedSheddingException>(async () =>
			await _sheddingsService.SetNewStatusForShedding(sheddingId, SheddingState.Approved));
	}

	[TestCase(SheddingState.Planned, SheddingState.Approved)]
	[TestCase(SheddingState.Planned, SheddingState.Confirmed)]
	[TestCase(SheddingState.Prepared, SheddingState.Planned)]
	[TestCase(SheddingState.Prepared, SheddingState.Confirmed)]
	[TestCase(SheddingState.Approved, SheddingState.Planned)]
	[TestCase(SheddingState.Approved, SheddingState.Prepared)]
	public async Task SetNewStatusForShedding_WhenIncorrectStateChange_NoValidNewStateForSheddingException
		(SheddingState currentState, SheddingState newState)
	{
		var shedding = new SheddingModel
		{
			Status = currentState
		};

		const int sheddingId = 5;
		_sheddingsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(shedding);

		Assert.ThrowsAsync<NoValidNewStateForSheddingException>(async () =>
			await _sheddingsService.SetNewStatusForShedding(sheddingId, newState));
	}

	[TestCase(SheddingState.Planned, SheddingState.Prepared)]
	[TestCase(SheddingState.Prepared, SheddingState.Approved)]
	[TestCase(SheddingState.Approved, SheddingState.Confirmed)]
	public async Task SetNewStatusForShedding_WhenAllRight_UpdateState(SheddingState currentState,
		SheddingState expectedNewState)
	{
		var sheddingModel = new SheddingModel
		{
			Status = currentState
		};

		const int sheddingId = 5;
		SheddingModel actualShedding = null;
		_sheddingsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(sheddingModel);

		_sheddingsRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<SheddingModel>()))
			.Callback((SheddingModel shedding) => actualShedding = shedding);

		await _sheddingsService.SetNewStatusForShedding(sheddingId, expectedNewState);

		Assert.NotNull(actualShedding);
		Assert.AreEqual(expectedNewState, actualShedding.Status);
	}

	[Test]
	public async Task DeleteSheddingAsync_WhenSheddingByIdNotExist_ThrowNotExistedSheddingException()
	{
		_sheddingsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(() => null);
		
		Assert.ThrowsAsync<NotExistedSheddingException>(async () =>
			await _sheddingsService.DeleteSheddingAsync(5));
	}
	
	[Test]
	public async Task DeleteSheddingAsync_WhenSheddingByIdExist_DeleteShedding()
	{
		var expectedShedding = new SheddingModel
		{
			Id = 5,
		};
		SheddingModel actualShedding = null;
		_sheddingsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
			.ReturnsAsync(expectedShedding);
		_sheddingsRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<SheddingModel>()))
			.Callback((SheddingModel shedding) => actualShedding = shedding);

		await _sheddingsService.DeleteSheddingAsync(5);
		
		Assert.NotNull(actualShedding);
		Assert.AreEqual(expectedShedding, actualShedding);
	}
}