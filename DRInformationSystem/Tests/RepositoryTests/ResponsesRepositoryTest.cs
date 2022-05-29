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
public class ResponsesRepositoryTest
{
	private ResponsesRepository _responsesRepository;
	private Mock<EntityDbContext> _dbContextMock;

	[SetUp]
	public void SetUpMethod()
	{
		_dbContextMock = new Mock<EntityDbContext>();
		_responsesRepository = new ResponsesRepository(_dbContextMock.Object);
	}

	[TearDown]
	public void TearDownMethod()
	{
		_responsesRepository = null;
		_dbContextMock = null;
	}

	[Test]
	public async Task GetByIdAsync_WhenDatabaseIsEmpty_ReturnNull()
	{
		IList<ResponseModel> responses = new List<ResponseModel>();
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var actualResponses = await _responsesRepository.GetByIdAsync(0);

		Assert.IsNull(actualResponses);
	}

	[Test]
	public async Task GetByIdAsync_WhenNoResponsesWithThisId_ReturnNull()
	{
		var responseModel = new ResponseModel
		{
			Id = 0,
		};
		IList<ResponseModel> responses = new List<ResponseModel> {responseModel};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var actualResponse = await _responsesRepository.GetByIdAsync(1);

		Assert.IsNull(actualResponse);
	}

	[Test]
	public async Task GetByIdAsync_WhenExistResponseWithThisId_ReturnResponse()
	{
		var id = 1;
		var expectedResponse = new ResponseModel
		{
			Id = id
		};
		IList<ResponseModel> responses = new List<ResponseModel> {expectedResponse};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var actualResponse = await _responsesRepository.GetByIdAsync(id);

		Assert.AreEqual(expectedResponse, actualResponse);
	}
}