using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.RepositoryTests;

[TestFixture]
public class OrdersRepositoryTest
{
	private OrdersRepository _ordersRepository;
	private Mock<EntityDbContext> _dbContextMock;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContextMock = new Mock<EntityDbContext>();
		_ordersRepository = new OrdersRepository(_dbContextMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_ordersRepository = null;
		_dbContextMock = null;
	}

	[Test]
	public async Task GetByIdAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<OrderModel> orders = new List<OrderModel>();
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var actualOrder = await _ordersRepository.GetByIdAsync(0);

		Assert.IsNull(actualOrder);
	}

	[Test]
	public async Task GetByIdAsync_WhenNoOrderWithThisId_ReturnNull()
	{
		var orderModel = new OrderModel
		{
			Id = 0,
		};
		IList<OrderModel> orders = new List<OrderModel> {orderModel};
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var actualOrder = await _ordersRepository.GetByIdAsync(1);

		Assert.IsNull(actualOrder);
	}

	[Test]
	public async Task GetByIdAsync_WhenExistOrderWithThisId_ReturnOrder()
	{
		var id = 1;
		var expectedOrder = new OrderModel
		{
			Id = id
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var actualOrder = await _ordersRepository.GetByIdAsync(id);

		Assert.AreEqual(expectedOrder, actualOrder);
	}

	[Test]
	public async Task GetAsync_WhenDatabaseIsEmpty_ReturnEmptyList()
	{
		IList<OrderModel> orders = new List<OrderModel>();
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var actualOrderList = await _ordersRepository.GetAsync();

		Assert.IsEmpty(actualOrderList);
	}

	[Test]
	public async Task GetAsync_WhenDatabaseIsNotEmpty_ReturnOrderList()
	{
		var expectedCount = 1;
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var actualOrderList = await _ordersRepository.GetAsync();
		var actualCount = actualOrderList.Count;
		var actualOrder = actualOrderList.FirstOrDefault();

		Assert.IsNotEmpty(actualOrderList);
		Assert.AreEqual(actualCount, expectedCount);
		Assert.AreEqual(actualOrder, expectedOrder);
	}

	[Test]
	public async Task GetAsync_WhenNoExitsOrderByAggregatorIdFilter_ReturnEmptyList()
	{
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue,
			AggregatorId = 2,
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var searchParams = new OrderSearchParams
		{
			AggregatorId = 1
		};
		var actualOrderList = await _ordersRepository.GetAsync(searchParams);

		Assert.IsEmpty(actualOrderList);
	}
	
	[Test]
	public async Task GetAsync_WhenExitsOrderByAggregatorIdFilter_ReturnOrderList()
	{
		var expectedCount = 1;
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue,
			AggregatorId = 1,
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var searchParams = new OrderSearchParams
		{
			AggregatorId = 1
		};
		var actualOrderList = await _ordersRepository.GetAsync(searchParams);
		var actualCount = actualOrderList.Count;
		var actualOrder = actualOrderList.FirstOrDefault();

		Assert.IsNotEmpty(actualOrderList);
		Assert.AreEqual(actualCount, expectedCount);
		Assert.AreEqual(actualOrder, expectedOrder);
	}
	
	[Test]
	public async Task GetAsync_WhenNoExitsOrderByConsumerIdFilter_ReturnEmptyList()
	{
		var response = new ResponseModel
		{
			ConsumerId = 1
		};
		var responseList = new List<ResponseModel> {response};
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue,
			AggregatorId = 2,
			Responses = responseList
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var searchParams = new OrderSearchParams
		{
			ConsumerId = 0
		};
		var actualOrderList = await _ordersRepository.GetAsync(searchParams);

		Assert.IsEmpty(actualOrderList);
	}

	[Test]
	public async Task GetAsync_WhenExitsOrderByConsumerIdFilter_ReturnOrderList()
	{
		var response = new ResponseModel
		{
			ConsumerId = 1
		};
		var responseList = new List<ResponseModel> {response};
		var expectedCount = 1;
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue,
			AggregatorId = 1,
			Responses = responseList
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders);

		var searchParams = new OrderSearchParams
		{
			ConsumerId = 1
		};
		var actualOrderList = await _ordersRepository.GetAsync(searchParams);
		var actualCount = actualOrderList.Count;
		var actualOrder = actualOrderList.FirstOrDefault();

		Assert.IsNotEmpty(actualOrderList);
		Assert.AreEqual(actualCount, expectedCount);
		Assert.AreEqual(actualOrder, expectedOrder);
	}

	[Test]
	public async Task SaveAsync_WhenEmptyOrderList_NewCountIsOne()
	{
		var expectedCount = 1;
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue
		};
		IList<OrderModel> orders = new List<OrderModel>();
		var dbSetMock = new Mock<DbSet<OrderModel>>();
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders, dbSetMock);
		dbSetMock.Setup(m => m.Add(It.IsAny<OrderModel>())).Callback((OrderModel order) => orders.Add(order));

		await _ordersRepository.SaveAsync(expectedOrder);
		var actualOrder = orders.FirstOrDefault();

		Assert.That(actualOrder, Is.EqualTo(expectedOrder));
		Assert.That(orders.Count, Is.EqualTo(expectedCount));
	}

	[Test]
	public async Task DeleteAsync_WhenOneInvite_NewCountIsZero()
	{
		var expectedCount = 0;
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		var dbSetMock = new Mock<DbSet<OrderModel>>();
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders, dbSetMock);
		dbSetMock.Setup(m => m.Remove(It.IsAny<OrderModel>())).Callback((OrderModel order) => orders.Remove(order));

		await _ordersRepository.DeleteAsync(expectedOrder);

		Assert.That(orders.Count, Is.EqualTo(expectedCount));
	}

	[Test]
	public async Task UpdateAsync_WhenOneInvite_UpdateMethodCalled()
	{
		var expectedOrder = new OrderModel
		{
			Id = 0,
			State = OrderState.NotFormatted,
			EndTimestamp = DateTime.MinValue,
			StartTimestamp = DateTime.MinValue
		};
		IList<OrderModel> orders = new List<OrderModel> {expectedOrder};
		var dbSetMock = new Mock<DbSet<OrderModel>>();
		_dbContextMock.Setup(x => x.Orders).ReturnsDbSet(orders, dbSetMock);
		OrderModel actualOrderModel = null;
		dbSetMock.Setup(m => m.Update(It.IsAny<OrderModel>())).Callback((OrderModel order) => actualOrderModel = order);

		await _ordersRepository.UpdateAsync(expectedOrder);

		Assert.That(actualOrderModel, Is.EqualTo(expectedOrder));
	}
}