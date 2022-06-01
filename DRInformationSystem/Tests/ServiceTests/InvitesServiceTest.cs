using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Moq;
using NUnit.Framework;

namespace Tests.ServiceTests;

[TestFixture]
public class InvitesServiceTest
{
	private InvitesService _invitesService;
	private Mock<IInvitesRepository> _invitesRepositoryMock;

	[SetUp]
	public void SetUpMethod()
	{
		_invitesRepositoryMock = new Mock<IInvitesRepository>();
		_invitesService = new InvitesService(_invitesRepositoryMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_invitesService = null;
		_invitesRepositoryMock = null;
	}

	[Test]
	public async Task InviteConsumerAsync_WhenNoLastInviteCode_SaveInviteWithDefaultCode()
	{
		const string expectedInviteCode = "1000-1001";
		InviteModel actualInviteModel = null;
		_invitesRepositoryMock
			.Setup(x => x.SaveAsync(It.IsAny<InviteModel>()))
			.Callback((InviteModel inviteCode) => actualInviteModel = inviteCode);

		await _invitesService.InviteConsumerAsync("defaultLogin");

		Assert.IsFalse(actualInviteModel.IsActivated);
		Assert.AreEqual(expectedInviteCode, actualInviteModel.Code);
	}

	[Test]
	public async Task InviteConsumerAsync_WhenLastInviteCodeExist_SaveInviteWithNextCode()
	{
		var lastInvite = new InviteModel
		{
			Code = "1000-1001"
		};

		const string expectedInviteCode = "1000-1002";
		InviteModel actualInviteModel = null;
		_invitesRepositoryMock.Setup(x => x.GetLastAsync())
			.ReturnsAsync(lastInvite);

		_invitesRepositoryMock
			.Setup(x => x.SaveAsync(It.IsAny<InviteModel>()))
			.Callback((InviteModel inviteCode) => actualInviteModel = inviteCode);

		await _invitesService.InviteConsumerAsync("defaultLogin");

		Assert.IsFalse(actualInviteModel.IsActivated);
		Assert.AreEqual(expectedInviteCode, actualInviteModel.Code);
	}

	[Test]
	public async Task ActivateInviteCodeAsync_WhenCodeNoExist_ReturnCodeNotExistResult()
	{
		var expectedResult = ActivationResult.CodeNotExist;
		_invitesRepositoryMock.Setup(x => x.GetByCodeAsync(It.IsAny<string>()))
			.ReturnsAsync(() => null);

		var actualResult = await _invitesService.ActivateInviteCodeAsync("1000-1001");

		Assert.AreEqual(expectedResult, actualResult);
	}

	[Test]
	public async Task ActivateInviteCodeAsync_WhenAlreadyActivated_ReturnAlreadyActivatedResult()
	{
		var inviteByCode = new InviteModel
		{
			Code = "1000-1001",
			IsActivated = true
		};

		var expectedResult = ActivationResult.AlreadyActivated;
		_invitesRepositoryMock.Setup(x => x.GetByCodeAsync(It.IsAny<string>()))
			.ReturnsAsync(inviteByCode);

		var actualResult = await _invitesService.ActivateInviteCodeAsync("1000-1001");

		Assert.AreEqual(expectedResult, actualResult);
	}

	[Test]
	public async Task ActivateInviteCodeAsync_WhenAllRight_UpdateInviteAndReturnSuccessesResult()
	{
		var inviteByCode = new InviteModel
		{
			Code = "1000-1001",
			IsActivated = false
		};

		var expectedResult = ActivationResult.Successes;
		InviteModel actualInviteModel = null;
		_invitesRepositoryMock.Setup(x => x.GetByCodeAsync(It.IsAny<string>()))
			.ReturnsAsync(inviteByCode);
		_invitesRepositoryMock
			.Setup(x => x.UpdateAsync(It.IsAny<InviteModel>()))
			.Callback((InviteModel inviteCode) => actualInviteModel = inviteCode);

		var actualResult = await _invitesService.ActivateInviteCodeAsync("1000-1001");

		Assert.IsTrue(actualInviteModel.IsActivated);
		Assert.AreEqual(expectedResult, actualResult);
	}
}