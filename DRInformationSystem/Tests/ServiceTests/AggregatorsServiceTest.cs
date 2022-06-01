using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer.Auth;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Moq;
using NUnit.Framework;

namespace Tests.ServiceTests;

[TestFixture]
public class AggregatorsServiceTest
{
	private AggregatorsService _aggregatorsService;
	private Mock<IAggregatorsRepository> _aggregatorsRepositoryMock;

	[SetUp]
	public void SetUpMethod()
	{
		_aggregatorsRepositoryMock = new Mock<IAggregatorsRepository>();
		_aggregatorsService = new AggregatorsService(_aggregatorsRepositoryMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_aggregatorsService = null;
		_aggregatorsRepositoryMock = null;
	}

	[Test]
	public async Task AuthorizeAggregatorAsync_WhenLoginIncorrect_ThrowsBadAuthException()
	{
		_aggregatorsRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => null);

		Assert.ThrowsAsync<BadAuthException>(async () => await _aggregatorsService.AuthorizeAggregatorAsync(
			string.Empty,
			string
				.Empty));
	}

	[Test]
	public async Task AuthorizeAggregatorAsync_WhenPasswordIncorrect_ThrowsBadAuthException()
	{
		const string login = "login";
		const string password = "teststring1";
		var aggregatorModel = new AggregatorModel
		{
			Id = 0,
			Login = login,
			PasswordHash = "3c8727e019a42b444667a587b6001251becadabbb36bfed8087a92c18882d111" // hash of "teststring"
		};

		_aggregatorsRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => aggregatorModel);

		Assert.ThrowsAsync<BadAuthException>(async () => await _aggregatorsService.AuthorizeAggregatorAsync(login,
			password));
	}

	[Test]
	public async Task AuthorizeAggregatorAsync_WhenAllCorrect_ReturnsToken()
	{
		const string expectedLogin = "login";
		const string expectedRole = AuthOptions.AggregatorRole;
		const string password = "teststring";
		var aggregatorModel = new AggregatorModel
		{
			Id = 0,
			Login = expectedLogin,
			PasswordHash = "3c8727e019a42b444667a587b6001251becadabbb36bfed8087a92c18882d111" // hash of "teststring"
		};

		_aggregatorsRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => aggregatorModel);

		var token = (JwtSecurityToken) await _aggregatorsService.AuthorizeAggregatorAsync(expectedLogin, password);
		var loginClaim = token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
		var roleClaim = token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

		Assert.AreEqual(expectedLogin, loginClaim);
		Assert.AreEqual(expectedRole, roleClaim);
	}

	[Test]
	public async Task GetAggregatorByLoginAsync_WhenAggregatorWithLoginExist_ReturnAggregator()
	{
		const string login = "login";
		var expectedAggregator = new AggregatorModel
		{
			Id = 0,
			Login = login,
			PasswordHash = "hash"
		};

		var repositoryMethodWasCalled = false;
		_aggregatorsRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.Callback(() => repositoryMethodWasCalled = true)
			.ReturnsAsync(() => expectedAggregator);

		var actualAggregator = await _aggregatorsService.GetAggregatorByLoginAsync(login);

		Assert.IsTrue(repositoryMethodWasCalled);
		Assert.AreEqual(expectedAggregator, actualAggregator);
	}
}