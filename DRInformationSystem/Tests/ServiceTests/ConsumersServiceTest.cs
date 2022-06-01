using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer.Auth;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Moq;
using NUnit.Framework;

namespace Tests.ServiceTests;

[TestFixture]
public class ConsumersServiceTest
{
	private ConsumersService _consumersService;
	private Mock<IConsumersRepository> _consumersRepositoryMock;
	private Mock<IInvitesService> _invitesServiceMock;

	[SetUp]
	public void SetUpMethod()
	{
		_consumersRepositoryMock = new Mock<IConsumersRepository>();
		_invitesServiceMock = new Mock<IInvitesService>();
		_consumersService = new ConsumersService(_consumersRepositoryMock.Object,
			_invitesServiceMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_consumersService = null;
		_consumersRepositoryMock = null;
	}

	[Test]
	public async Task AuthorizeConsumerAsync_WhenLoginIncorrect_ThrowsBadAuthException()
	{
		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => null);

		Assert.ThrowsAsync<BadAuthException>(async () => await _consumersService.AuthorizeConsumerAsync(
			string.Empty, string.Empty));
	}

	[Test]
	public async Task AuthorizeConsumerAsync_WhenPasswordIncorrect_ThrowsBadAuthException()
	{
		const string login = "login";
		const string password = "teststring1";
		var consumerModel = new ConsumerModel
		{
			Id = 0,
			Login = login,
			PasswordHash = "3c8727e019a42b444667a587b6001251becadabbb36bfed8087a92c18882d111" // hash of "teststring"
		};

		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => consumerModel);

		Assert.ThrowsAsync<BadAuthException>(async () => await _consumersService.AuthorizeConsumerAsync(login,
			password));
	}

	[Test]
	public async Task AuthorizeConsumerAsync_WhenAllCorrect_ReturnsToken()
	{
		const string expectedLogin = "login";
		const string expectedRole = AuthOptions.ConsumerRole;
		const string password = "teststring";
		var consumerModel = new ConsumerModel
		{
			Id = 0,
			Login = expectedLogin,
			PasswordHash = "3c8727e019a42b444667a587b6001251becadabbb36bfed8087a92c18882d111" // hash of "teststring"
		};

		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => consumerModel);

		var token = (JwtSecurityToken) await _consumersService.AuthorizeConsumerAsync(expectedLogin, password);
		var loginClaim = token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
		var roleClaim = token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

		Assert.AreEqual(expectedLogin, loginClaim);
		Assert.AreEqual(expectedRole, roleClaim);
	}

	[Test]
	public async Task RegisterConsumerAsync_WhenAlreadyUsedLogin_ThrowAlreadyUsedLoginException()
	{
		const string login = "login";
		var consumerModel = new ConsumerModel
		{
			Id = 0,
			Login = login
		};

		var registerConsumerData = new RegisterConsumerData
		{
			Login = login
		};

		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => consumerModel);

		Assert.ThrowsAsync<AlreadyUsedLoginException>(async () =>
			await _consumersService.RegisterConsumerAsync(registerConsumerData));
	}

	[Test]
	public async Task RegisterConsumerAsync_WhenInviteCodeAlreadyActivated_ThrowsAlreadyActivatedInviteCodeException()
	{
		const string inviteCode = "code";
		;
		var registerConsumerData = new RegisterConsumerData
		{
			InviteCode = inviteCode
		};

		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => null);

		_invitesServiceMock
			.Setup(x => x.ActivateInviteCodeAsync(It.IsAny<string>()))
			.ReturnsAsync(() => ActivationResult.AlreadyActivated);

		Assert.ThrowsAsync<AlreadyActivatedInviteCodeException>(async () =>
			await _consumersService.RegisterConsumerAsync(registerConsumerData));
	}

	[Test]
	public async Task RegisterConsumerAsync_WhenInviteCodeNotExits_ThrowsCodeNoExistException()
	{
		const string expectedLogin = "login";
		const string password = "teststring";
		const string expectedPasswordHash = "3c8727e019a42b444667a587b6001251becadabbb36bfed8087a92c18882d111";
		const string inviteCode = "code";
		var registerConsumerData = new RegisterConsumerData
		{
			Login = expectedLogin,
			Password = password,
			InviteCode = inviteCode
		};

		ConsumerModel actualConsumerModel = null;
		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => null);

		_invitesServiceMock
			.Setup(x => x.ActivateInviteCodeAsync(It.IsAny<string>()))
			.ReturnsAsync(() => ActivationResult.CodeNotExist);

		Assert.ThrowsAsync<CodeNoExistException>(async () =>
			await _consumersService.RegisterConsumerAsync(registerConsumerData));
	}

	[Test]
	public async Task RegisterConsumerAsync_WhenAllCorrect_ThrowsCodeNoExistException()
	{
		const string expectedLogin = "login";
		const string password = "teststring";
		const string expectedPasswordHash = "3c8727e019a42b444667a587b6001251becadabbb36bfed8087a92c18882d111";
		const string inviteCode = "code";
		var registerConsumerData = new RegisterConsumerData
		{
			Login = expectedLogin,
			Password = password,
			InviteCode = inviteCode
		};

		ConsumerModel actualConsumerModel = null;
		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.ReturnsAsync(() => null);

		_invitesServiceMock
			.Setup(x => x.ActivateInviteCodeAsync(It.IsAny<string>()))
			.ReturnsAsync(() => ActivationResult.Successes);

		_consumersRepositoryMock
			.Setup(x => x.SaveAsync(It.IsAny<ConsumerModel>()))
			.Callback((ConsumerModel consumer) => actualConsumerModel = consumer);

		await _consumersService.RegisterConsumerAsync(registerConsumerData);

		Assert.AreEqual(expectedLogin, actualConsumerModel.Login);
		Assert.AreEqual(expectedPasswordHash, actualConsumerModel.PasswordHash);
	}

	[Test]
	public async Task GetConsumersAsync_WhenConsumersExist_ReturnConsumerList()
	{
		var expectedConsumer = new ConsumerModel
		{
			Id = 0,
			Login = "login",
			PasswordHash = "hash"
		};

		var expectedConsumerList = new List<ConsumerModel> {expectedConsumer};
		var repositoryMethodWasCalled = false;
		_consumersRepositoryMock
			.Setup(x => x.GetAsync())
			.Callback(() => repositoryMethodWasCalled = true)
			.ReturnsAsync(() => expectedConsumerList);

		var actualConsumerList = await _consumersService.GetConsumersAsync();

		Assert.IsTrue(repositoryMethodWasCalled);
		Assert.AreEqual(expectedConsumerList, actualConsumerList);
	}

	[Test]
	public async Task GetConsumerByLoginAsync_WhenConsumerWithLoginExist_ReturnConsumer()
	{
		const string login = "login";
		var expectedConsumer = new ConsumerModel
		{
			Id = 0,
			Login = login,
			PasswordHash = "hash"
		};

		var repositoryMethodWasCalled = false;
		_consumersRepositoryMock
			.Setup(x => x.GetByLoginAsync(It.IsAny<string>()))
			.Callback(() => repositoryMethodWasCalled = true)
			.ReturnsAsync(() => expectedConsumer);

		var actualConsumer = await _consumersService.GetConsumersByLoginAsync(login);

		Assert.IsTrue(repositoryMethodWasCalled);
		Assert.AreEqual(expectedConsumer, actualConsumer);
	}
}