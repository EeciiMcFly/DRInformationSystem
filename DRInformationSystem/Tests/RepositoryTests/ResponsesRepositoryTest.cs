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

		var actualResponse = await _responsesRepository.GetByIdAsync(0);

		Assert.IsNull(actualResponse);
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

	[Test]
	public async Task GetRangeByIdAsync_WhenDatabaseIsEmpty_ReturnEmptyList()
	{
		IList<ResponseModel> responses = new List<ResponseModel>();
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var responsesIds = new List<long> {0};
		var actualResponses = await _responsesRepository.GetRangeByIdsAsync(responsesIds);

		Assert.IsEmpty(actualResponses);
	}

	[Test]
	public async Task GetRangeByIdAsync_WhenNoResponsesWithThisId_ReturnEmptyList()
	{
		var responseModel = new ResponseModel
		{
			Id = 0,
		};

		IList<ResponseModel> responses = new List<ResponseModel> {responseModel};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var responsesIds = new List<long> {1};
		var actualResponse = await _responsesRepository.GetRangeByIdsAsync(responsesIds);

		Assert.IsEmpty(actualResponse);
	}

	[Test]
	public async Task GetRangeByIdAsync_WhenExistResponseWithThisId_ReturnResponse()
	{
		var id = 1;
		var expectedResponse = new ResponseModel
		{
			Id = id
		};

		IList<ResponseModel> responses = new List<ResponseModel> {expectedResponse};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var responsesIds = new List<long> {id};
		var actualResponses = await _responsesRepository.GetRangeByIdsAsync(responsesIds);
		var actualResponse = actualResponses.FirstOrDefault();

		Assert.IsNotEmpty(actualResponses);
		Assert.AreEqual(actualResponse, expectedResponse);
	}

	[Test]
	public async Task GetAsync_WhenNoExitsResponseByOrderIdFilter_ReturnEmptyList()
	{
		var response = new ResponseModel
		{
			OrderId = 1,
			ConsumerId = 1
		};

		IList<ResponseModel> responses = new List<ResponseModel> {response};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var searchParams = new ResponseSearchParams
		{
			OrderId = 0
		};

		var actualOrderList = await _responsesRepository.GetAsync(searchParams);

		Assert.IsEmpty(actualOrderList);
	}

	[Test]
	public async Task GetAsync_WhenExitsResponseByOrderIdFilter_ReturnResponseList()
	{
		var expectedCount = 1;
		var expectedResponse = new ResponseModel
		{
			OrderId = 1,
			ConsumerId = 1
		};

		IList<ResponseModel> responses = new List<ResponseModel> {expectedResponse};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var searchParams = new ResponseSearchParams
		{
			OrderId = 1
		};

		var actualResponseList = await _responsesRepository.GetAsync(searchParams);
		var actualCount = actualResponseList.Count;
		var actualResponse = actualResponseList.FirstOrDefault();

		Assert.IsNotEmpty(actualResponseList);
		Assert.AreEqual(expectedCount, actualCount);
		Assert.AreEqual(expectedResponse, actualResponse);
	}

	[Test]
	public async Task GetAsync_WhenNoExitsResponseByConsumerIdFilter_ReturnEmptyList()
	{
		var response = new ResponseModel
		{
			OrderId = 1,
			ConsumerId = 1
		};

		IList<ResponseModel> responses = new List<ResponseModel> {response};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var searchParams = new ResponseSearchParams
		{
			ConsumerId = 0
		};

		var actualOrderList = await _responsesRepository.GetAsync(searchParams);

		Assert.IsEmpty(actualOrderList);
	}

	[Test]
	public async Task GetAsync_WhenExitsResponseByConsumerIdFilter_ReturnResponseList()
	{
		var expectedCount = 1;
		var expectedResponse = new ResponseModel
		{
			OrderId = 1,
			ConsumerId = 1
		};

		IList<ResponseModel> responses = new List<ResponseModel> {expectedResponse};
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses);

		var searchParams = new ResponseSearchParams
		{
			ConsumerId = 1
		};

		var actualResponseList = await _responsesRepository.GetAsync(searchParams);
		var actualCount = actualResponseList.Count;
		var actualResponse = actualResponseList.FirstOrDefault();

		Assert.IsNotEmpty(actualResponseList);
		Assert.AreEqual(expectedCount, actualCount);
		Assert.AreEqual(expectedResponse, actualResponse);
	}

	[Test]
	public async Task SaveAsync_WhenEmptyResponseList_NewCountIsOne()
	{
		var expectedCount = 1;
		var expectedResponseModel = new ResponseModel
		{
			Id = 1,
			ReduceData = new List<long> {0}
		};

		IList<ResponseModel> responses = new List<ResponseModel>();
		var dbSetMock = new Mock<DbSet<ResponseModel>>();
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses, dbSetMock);
		dbSetMock.Setup(m => m.Add(It.IsAny<ResponseModel>()))
			.Callback((ResponseModel response) => responses.Add(response));

		await _responsesRepository.SaveAsync(expectedResponseModel);
		var actualResponse = responses.FirstOrDefault();

		Assert.AreEqual(expectedResponseModel, actualResponse);
		Assert.AreEqual(expectedCount, responses.Count);
	}

	[Test]
	public async Task DeleteAsync_WhenOneInvite_NewCountIsZero()
	{
		var expectedCount = 0;
		var responseModel = new ResponseModel
		{
			Id = 1,
			ReduceData = new List<long> {0}
		};

		IList<ResponseModel> responses = new List<ResponseModel>();
		var dbSetMock = new Mock<DbSet<ResponseModel>>();
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses, dbSetMock);
		dbSetMock.Setup(m => m.Remove(It.IsAny<ResponseModel>()))
			.Callback((ResponseModel response) => responses.Remove(response));

		await _responsesRepository.DeleteAsync(responseModel);

		Assert.AreEqual(expectedCount, responses.Count);
	}

	[Test]
	public async Task UpdateAsync_WhenOneInvite_UpdateMethodCalled()
	{
		var expectedResponseModel = new ResponseModel
		{
			Id = 1,
			ReduceData = new List<long> {0}
		};

		IList<ResponseModel> responses = new List<ResponseModel> {expectedResponseModel};
		var dbSetMock = new Mock<DbSet<ResponseModel>>();
		_dbContextMock.Setup(x => x.Responses).ReturnsDbSet(responses, dbSetMock);
		ResponseModel actualResponseModel = null;
		dbSetMock.Setup(m => m.Update(It.IsAny<ResponseModel>()))
			.Callback((ResponseModel response) => actualResponseModel = response);

		await _responsesRepository.UpdateAsync(expectedResponseModel);

		Assert.AreEqual(expectedResponseModel, actualResponseModel);
	}
}