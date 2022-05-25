using DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tests.RepositoryTests;

public static class RepositoryTestHelper
{
	public static EntityDbContext CreateDbContextForTest()
	{
		var contextOptions = new DbContextOptionsBuilder<EntityDbContext>()
			.UseInMemoryDatabase("test_database")
			.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		var context = new EntityDbContext(contextOptions);
		return context;
	}
}