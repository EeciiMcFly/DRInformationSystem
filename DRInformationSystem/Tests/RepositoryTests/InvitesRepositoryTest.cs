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

public class InvitesRepositoryTest
{
	private InvitesRepository _invitesRepository;
	private Mock<EntityDbContext> _dbContextMock;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContextMock = new Mock<EntityDbContext>();
		_invitesRepository = new InvitesRepository(_dbContextMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_invitesRepository = null;
		_dbContextMock = null;
	}

	[Test]
	public async Task GetByCodeAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<InviteModel> invites = new List<InviteModel>();
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetByCodeAsync(string.Empty);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByCodeAsync_WhenNoInviteWithThisCode_ReturnNull()
	{
		var inviteModel = new InviteModel
		{
			Code = "default_code"
		};
		IList<InviteModel> invites = new List<InviteModel> {inviteModel};
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetByCodeAsync(string.Empty);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByCodeAsync_WhenExistInviteWithThisCode_ReturnAggregator()
	{
		var code = "default_code";
		var expectedInvite = new InviteModel
		{
			Code = code
		};
		IList<InviteModel> invites = new List<InviteModel> {expectedInvite};
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetByCodeAsync(code);

		Assert.AreEqual(expectedInvite, actualConsumer);
	}

	[Test]
	public async Task GetLastAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<InviteModel> invites = new List<InviteModel>();
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetLastAsync();

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetLastAsync_WhenInvitesExist_ReturnInvite()
	{
		var expectedInvite = new InviteModel
		{
			Code = "default_code"
		};
		IList<InviteModel> invites = new List<InviteModel> {expectedInvite};
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetLastAsync();

		Assert.AreEqual(expectedInvite, actualConsumer);
	}

	[Test]
	public async Task GetByIdAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<InviteModel> invites = new List<InviteModel>();
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetByIdAsync(0);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByIdAsync_WhenNoInviteWithThisCode_ReturnNull()
	{
		var inviteModel = new InviteModel
		{
			Id = 0,
			Code = "default_code"
		};
		IList<InviteModel> invites = new List<InviteModel> {inviteModel};
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetByIdAsync(1);

		Assert.IsNull(actualConsumer);
	}

	[Test]
	public async Task GetByIdAsync_WhenExistInviteWithThisCode_ReturnAggregator()
	{
		var id = 1;
		var expectedInvite = new InviteModel
		{
			Id = id
		};
		IList<InviteModel> invites = new List<InviteModel> {expectedInvite};
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites);

		var actualConsumer = await _invitesRepository.GetByIdAsync(id);

		Assert.AreEqual(expectedInvite, actualConsumer);
	}

	[Test]
	public async Task SaveAsync_WhenEmptyInvitesList_NewCountIsOne()
	{
		var expectedCount = 1;
		var expectedInviteModel = new InviteModel
		{
			Id = 1,
			Code = "defaultLogin",
		};
		IList<InviteModel> invites = new List<InviteModel>();
		var dbSetMock = new Mock<DbSet<InviteModel>>();
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites, dbSetMock);
		dbSetMock.Setup(m => m.Add(It.IsAny<InviteModel>())).Callback((InviteModel invite) => invites.Add(invite));

		await _invitesRepository.SaveAsync(expectedInviteModel);
		var actualInvite = invites.FirstOrDefault();

		Assert.That(actualInvite, Is.EqualTo(expectedInviteModel));
		Assert.That(invites.Count, Is.EqualTo(expectedCount));
	}
	
	[Test]
	public async Task DeleteAsync_WhenOneInvite_NewCountIsZero()
	{
		var expectedCount = 0;
		var inviteModel = new InviteModel
		{
			Id = 1,
			Code = "defaultLogin",
		};
		IList<InviteModel> invites = new List<InviteModel> {inviteModel};
		var dbSetMock = new Mock<DbSet<InviteModel>>();
		_dbContextMock.Setup(x => x.Invites).ReturnsDbSet(invites, dbSetMock);
		dbSetMock.Setup(m => m.Remove(It.IsAny<InviteModel>())).Callback((InviteModel invite) => invites.Remove(invite));

		await _invitesRepository.DeleteAsync(inviteModel);
		
		Assert.That(invites.Count, Is.EqualTo(expectedCount));
	}
}