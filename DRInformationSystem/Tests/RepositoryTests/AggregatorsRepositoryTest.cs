using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.RepositoryTests;

[TestFixture]
public class AggregatorsRepositoryTest
{
	private AggregatorsRepository _aggregatorsRepository;
	private Mock<EntityDbContext> _dbContextMock;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContextMock = new Mock<EntityDbContext>();
		_aggregatorsRepository = new AggregatorsRepository(_dbContextMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_aggregatorsRepository = null;
		_dbContextMock = null;
	}

	[Test]
	public async Task GetByLoginAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<AggregatorModel> aggregators = new List<AggregatorModel>();
		_dbContextMock.Setup(x => x.Aggregators).ReturnsDbSet(aggregators);

		var actualAggregator = await _aggregatorsRepository.GetByLoginAsync(string.Empty);

		Assert.IsNull(actualAggregator);
	}

	[Test]
	public async Task GetByLoginAsync_WhenNoAggregatorWithThisLogin_ReturnNull()
	{
		var aggregatorModel = new AggregatorModel
		{
			Login = "defaultLogin"
		};

		IList<AggregatorModel> aggregators = new List<AggregatorModel> {aggregatorModel};
		_dbContextMock.Setup(x => x.Aggregators).ReturnsDbSet(aggregators);

		var actualAggregator = await _aggregatorsRepository.GetByLoginAsync(string.Empty);

		Assert.IsNull(actualAggregator);
	}

	[Test]
	public async Task GetByLoginAsync_WhenExistAggregatorWithThisLogin_ReturnAggregator()
	{
		var login = "defaultLogin";
		var expectedAggregator = new AggregatorModel
		{
			Id = 1,
			Login = login,
			PasswordHash = string.Empty
		};

		IList<AggregatorModel> aggregators = new List<AggregatorModel> {expectedAggregator};
		_dbContextMock.Setup(x => x.Aggregators).ReturnsDbSet(aggregators);

		var actualAggregator = await _aggregatorsRepository.GetByLoginAsync(login);

		Assert.AreEqual(expectedAggregator, actualAggregator);
	}

	[Test]
	public async Task GetByIdAsync_WhenNoAggregatorWithThisId_ReturnNull()
	{
		var aggregatorModel = new AggregatorModel
		{
			Id = 0
		};

		IList<AggregatorModel> aggregators = new List<AggregatorModel> {aggregatorModel};
		_dbContextMock.Setup(x => x.Aggregators).ReturnsDbSet(aggregators);

		var actualAggregator = await _aggregatorsRepository.GetByIdAsync(1);

		Assert.IsNull(actualAggregator);
	}

	[Test]
	public async Task GetByIdAsync_WhenExistAggregatorWithThisId_ReturnAggregator()
	{
		const int id = 0;
		var expectedAggregator = new AggregatorModel
		{
			Id = id,
			Login = "login",
			PasswordHash = string.Empty
		};

		IList<AggregatorModel> aggregators = new List<AggregatorModel> {expectedAggregator};
		_dbContextMock.Setup(x => x.Aggregators).ReturnsDbSet(aggregators);

		var actualAggregator = await _aggregatorsRepository.GetByIdAsync(id);

		Assert.AreEqual(expectedAggregator, actualAggregator);
	}
}