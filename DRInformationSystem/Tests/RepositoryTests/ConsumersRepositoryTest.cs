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
public class ConsumersRepositoryTest
{
	private ConsumersRepository _consumersRepository;
	private Mock<EntityDbContext> _dbContextMock;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContextMock = new Mock<EntityDbContext>();
		_consumersRepository = new ConsumersRepository(_dbContextMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_consumersRepository = null;
		_dbContextMock = null;
	}

	[Test]
	public async Task GetByLoginAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<ConsumerModel> consumers = new List<ConsumerModel>();
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumersRepository.GetByLoginAsync(string.Empty);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByLoginAsync_WhenNoConsumerWithThisLogin_ReturnNull()
	{
		var consumerModel = new ConsumerModel
		{
			Login = "defaultLogin"
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {consumerModel};
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumersRepository.GetByLoginAsync(string.Empty);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByLoginAsync_WhenExistConsumerWithThisLogin_ReturnConsumer()
	{
		var login = "defaultLogin";
		var expectedConsumer = new ConsumerModel
		{
			Login = login
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {expectedConsumer};
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumersRepository.GetByLoginAsync(login);

		Assert.AreEqual(expectedConsumer, actualConsumer);
	}

	[Test]
	public async Task GetByIdAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<ConsumerModel> consumers = new List<ConsumerModel>();
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumersRepository.GetByIdAsync(0);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByIdAsync_WhenNoInviteWithThisId_ReturnNull()
	{
		var consumerModel = new ConsumerModel
		{
			Id = 0,
			Login = "defaultLogin"
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {consumerModel};
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumersRepository.GetByIdAsync(1);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByIdAsync_WhenExistInviteWithThisId_ReturnInvite()
	{
		var id = 1;
		var expectedConsumer = new ConsumerModel
		{
			Id = id,
			Login = "defaultLogin"
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {expectedConsumer};
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumersRepository.GetByIdAsync(id);

		Assert.AreEqual(expectedConsumer, actualConsumer);
	}

	[Test]
	public async Task GetAsync_WhenDatabaseIsEmpty_ReturnEmptyList()
	{
		IList<ConsumerModel> consumer = new List<ConsumerModel>();
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumer);

		var actualConsumerList = await _consumersRepository.GetAsync();

		Assert.IsEmpty(actualConsumerList);
	}

	[Test]
	public async Task GetAsync_WhenDatabaseIsNotEmpty_ReturnOrderList()
	{
		var expectedCount = 1;
		var expectedConsumer = new ConsumerModel
		{
			Id = 1,
			Login = "defaultLogin",
			PasswordHash = string.Empty
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {expectedConsumer};
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumerList = await _consumersRepository.GetAsync();
		var actualCount = actualConsumerList.Count;
		var actualConsumer = actualConsumerList.FirstOrDefault();

		Assert.IsNotEmpty(actualConsumerList);
		Assert.AreEqual(expectedCount, actualCount);
		Assert.AreEqual(expectedConsumer, actualConsumer);
	}

	[Test]
	public async Task SaveAsync_WhenEmptyConsumersList_NewCountIsOne()
	{
		var expectedCount = 1;
		var expectedConsumerModel = new ConsumerModel
		{
			Id = 1,
			Login = "defaultLogin",
			PasswordHash = string.Empty
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel>();
		var dbSetMock = new Mock<DbSet<ConsumerModel>>();
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers, dbSetMock);
		dbSetMock.Setup(m => m.Add(It.IsAny<ConsumerModel>()))
			.Callback((ConsumerModel consumer) => consumers.Add(consumer));

		await _consumersRepository.SaveAsync(expectedConsumerModel);
		var actualConsumer = consumers.FirstOrDefault();

		Assert.AreEqual(expectedConsumerModel, actualConsumer);
		Assert.AreEqual(expectedCount, consumers.Count);
	}

	[Test]
	public async Task DeleteAsync_WhenOneInvite_NewCountIsZero()
	{
		var expectedCount = 0;
		var consumerModel = new ConsumerModel
		{
			Id = 1,
			Login = "defaultLogin",
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel>();
		var dbSetMock = new Mock<DbSet<ConsumerModel>>();
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers, dbSetMock);
		dbSetMock.Setup(m => m.Remove(It.IsAny<ConsumerModel>()))
			.Callback((ConsumerModel consumer) => consumers.Remove(consumer));

		await _consumersRepository.DeleteAsync(consumerModel);

		Assert.AreEqual(expectedCount, consumers.Count);
	}

	[Test]
	public async Task UpdateAsync_WhenOneInvite_UpdateMethodCalled()
	{
		var expectedConsumer = new ConsumerModel
		{
			Id = 1,
			Login = "defaultLogin",
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {expectedConsumer};
		var dbSetMock = new Mock<DbSet<ConsumerModel>>();
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers, dbSetMock);
		ConsumerModel actualConsumer = null;
		dbSetMock.Setup(m => m.Update(It.IsAny<ConsumerModel>()))
			.Callback((ConsumerModel consumer) => actualConsumer = consumer);

		await _consumersRepository.UpdateAsync(expectedConsumer);

		Assert.AreEqual(expectedConsumer, actualConsumer);
	}
}