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

public class SheddingsRepositoryTest
{
	private SheddingsRepository _sheddingsRepository;
	private Mock<EntityDbContext> _dbContextMock;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContextMock = new Mock<EntityDbContext>();
		_sheddingsRepository = new SheddingsRepository(_dbContextMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_sheddingsRepository = null;
		_dbContextMock = null;
	}

	[Test]
	public async Task GetByIdAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<SheddingModel> sheddings = new List<SheddingModel>();
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings);

		var actualShedding = await _sheddingsRepository.GetByIdAsync(0);

		Assert.IsNull(actualShedding);
	}

	[Test]
	public async Task GetByIdAsync_WhenNoResponsesWithThisId_ReturnNull()
	{
		var shedding = new SheddingModel
		{
			Id = 0,
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {shedding};
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings);

		var actualSheddings = await _sheddingsRepository.GetByIdAsync(1);

		Assert.IsNull(actualSheddings);
	}

	[Test]
	public async Task GetByIdAsync_WhenExistResponseWithThisId_ReturnResponse()
	{
		var id = 1;
		var expectedShedding = new SheddingModel
		{
			Id = id
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {expectedShedding};
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings);

		var actualShedding = await _sheddingsRepository.GetByIdAsync(id);

		Assert.AreEqual(expectedShedding, actualShedding);
	}

	[Test]
	public async Task GetAsync_WhenNoExitsSheddingByOrderIdFilter_ReturnEmptyList()
	{
		var expectedShedding = new SheddingModel
		{
			Id = 0,
			OrderId = 1,
			ConsumerId = 1,
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {expectedShedding};
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings);

		var searchParams = new SheddingSearchParams
		{
			OrderId = 2
		};
		var actualSheddingList = await _sheddingsRepository.GetAsync(searchParams);

		Assert.IsEmpty(actualSheddingList);
	}

	[Test]
	public async Task GetAsync_WhenExitsSheddingByOrderIdFilter_ReturnSheddingList()
	{
		var expectedCount = 1;
		var expectedShedding = new SheddingModel
		{
			Id = 0,
			OrderId = 1,
			ConsumerId = 1,
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {expectedShedding};
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings);

		var searchParams = new SheddingSearchParams
		{
			OrderId = 1
		};
		var actualSheddingList = await _sheddingsRepository.GetAsync(searchParams);
		var actualCount = actualSheddingList.Count;
		var actualShedding = actualSheddingList.FirstOrDefault();

		Assert.IsNotEmpty(actualSheddingList);
		Assert.AreEqual(expectedCount, actualCount);
		Assert.AreEqual(expectedShedding, actualShedding);
	}

	[Test]
	public async Task GetAsync_WhenNoExitsSheddingByConsumerIdFilter_ReturnEmptyList()
	{
		var expectedShedding = new SheddingModel
		{
			Id = 0,
			ConsumerId = 1
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {expectedShedding};
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings);

		var searchParams = new SheddingSearchParams
		{
			ConsumerId = 0
		};
		var actualOrderList = await _sheddingsRepository.GetAsync(searchParams);

		Assert.IsEmpty(actualOrderList);
	}

	[Test]
	public async Task GetAsync_WhenExitsSheddingByConsumerIdFilter_ReturnSheddingList()
	{
		var expectedCount = 1;
		var expectedShedding = new SheddingModel
		{
			Id = 0,
			OrderId = 1,
			ConsumerId = 1,
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {expectedShedding};
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings);

		var searchParams = new SheddingSearchParams
		{
			ConsumerId = 1
		};
		var actualSheddingList = await _sheddingsRepository.GetAsync(searchParams);
		var actualCount = actualSheddingList.Count;
		var actualShedding = actualSheddingList.FirstOrDefault();

		Assert.IsNotEmpty(actualSheddingList);
		Assert.AreEqual(expectedCount, actualCount);
		Assert.AreEqual(expectedShedding, actualShedding);
	}
	
	[Test]
	public async Task SaveAsync_WhenEmptyOrderList_NewCountIsOne()
	{
		var expectedCount = 1;
		var expectedShedding = new SheddingModel
		{
			Id = 0,
			StartTimestamp = DateTime.MinValue
		};
		IList<SheddingModel> sheddings = new List<SheddingModel>();
		var dbSetMock = new Mock<DbSet<SheddingModel>>();
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings, dbSetMock);
		dbSetMock.Setup(m => m.Add(It.IsAny<SheddingModel>())).Callback((SheddingModel shedding) => sheddings.Add(shedding));

		await _sheddingsRepository.SaveAsync(expectedShedding);
		var actualShedding = sheddings.FirstOrDefault();

		Assert.AreEqual(expectedShedding, actualShedding);
		Assert.AreEqual(expectedCount, sheddings.Count);
	}

	[Test]
	public async Task DeleteAsync_WhenOneInvite_NewCountIsZero()
	{
		var expectedCount = 0;
		var expectedShedding = new SheddingModel
		{
			Id = 0,
			StartTimestamp = DateTime.MinValue
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {expectedShedding};
		var dbSetMock = new Mock<DbSet<SheddingModel>>();
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings, dbSetMock);
		dbSetMock.Setup(m => m.Remove(It.IsAny<SheddingModel>())).Callback((SheddingModel shedding) => sheddings.Remove(shedding));

		await _sheddingsRepository.DeleteAsync(expectedShedding);

		Assert.AreEqual(expectedCount, sheddings.Count);
	}

	[Test]
	public async Task UpdateAsync_WhenOneInvite_UpdateMethodCalled()
	{
		var expectedShedding = new SheddingModel
		{
			Id = 0,
			StartTimestamp = DateTime.MinValue
		};
		IList<SheddingModel> sheddings = new List<SheddingModel> {expectedShedding};
		var dbSetMock = new Mock<DbSet<SheddingModel>>();
		_dbContextMock.Setup(x => x.Sheddings).ReturnsDbSet(sheddings, dbSetMock);
		SheddingModel actualShedding = null;
		dbSetMock.Setup(m => m.Update(It.IsAny<SheddingModel>()))
			.Callback((SheddingModel shedding) => actualShedding = shedding);

		await _sheddingsRepository.UpdateAsync(expectedShedding);

		Assert.AreEqual(expectedShedding, actualShedding);
	}
}