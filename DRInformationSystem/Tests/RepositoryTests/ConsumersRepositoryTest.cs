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
public class ConsumersRepositoryTest
{
	private ConsumerRepository _consumerRepository;
	private Mock<EntityDbContext> _dbContextMock;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContextMock = new Mock<EntityDbContext>();
		_consumerRepository = new ConsumerRepository(_dbContextMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_consumerRepository = null;
		_dbContextMock = null;
	}

	[Test]
	public async Task GetAggregatorByLoginAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<ConsumerModel> consumers = new List<ConsumerModel>();
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumerRepository.GetByLoginAsync(string.Empty);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetAggregatorByLoginAsync_WhenNoAggregatorWithThisLogin_ReturnNull()
	{
		var consumerModel = new ConsumerModel
		{
			Login = "defaultLogin"
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {consumerModel};
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumerRepository.GetByLoginAsync(string.Empty);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetAggregatorByLoginAsync_WhenExistAggregatorWithThisLogin_ReturnAggregator()
	{
		var login = "defaultLogin";
		var expectedConsumer = new ConsumerModel
		{
			Login = login
		};
		IList<ConsumerModel> consumers = new List<ConsumerModel> {expectedConsumer};
		_dbContextMock.Setup(x => x.Consumers).ReturnsDbSet(consumers);

		var actualConsumer = await _consumerRepository.GetByLoginAsync(login);

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
		
		await _consumerRepository.SaveAsync(expectedConsumerModel);
		var actualAggregator = consumers.FirstOrDefault();

		Assert.That(actualAggregator, Is.EqualTo(expectedConsumerModel));
		Assert.That(consumers.Count, Is.EqualTo(expectedCount));
	}
}