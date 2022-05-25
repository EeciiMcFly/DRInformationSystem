using System.Threading.Tasks;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Repositories;
using NUnit.Framework;

namespace Tests.RepositoryTests;

public class AggregatorsRepositoryTest
{
	private AggregatorsRepository _aggregatorsRepository;
	private EntityDbContext _dbContext;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContext = RepositoryTestHelper.CreateDbContextForTest();
		_dbContext.Database.EnsureDeleted();
		_dbContext.Database.EnsureCreated();
		var _aggregatorsRepository = new AggregatorsRepository(_dbContext);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_aggregatorsRepository = null;
		_dbContext.Dispose();
	}

	[Test]
	public async Task GetAggregatorByLoginAsync_WhereDatabaseIsEmpty_ReturnNull()
	{
		var actualAggregator = await _aggregatorsRepository.GetByLoginAsync(string.Empty);

		Assert.IsNull(actualAggregator);
	}
}