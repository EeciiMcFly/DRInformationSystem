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
		dbSetMock.Setup(m => m.Add(It.IsAny<ConsumerModel>())).Callback((ConsumerModel consumer) => consumers.Add(consumer));
		
		await _consumersRepository.SaveAsync(expectedConsumerModel);
		var actualConsumer = consumers.FirstOrDefault();

		Assert.That(actualConsumer, Is.EqualTo(expectedConsumerModel));
		Assert.That(consumers.Count, Is.EqualTo(expectedCount));
	}
}