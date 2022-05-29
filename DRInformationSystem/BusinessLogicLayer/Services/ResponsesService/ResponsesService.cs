using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class ResponsesService : IResponsesService
{
	private readonly IResponsesRepository _responsesRepository;

	public ResponsesService(IResponsesRepository responsesRepository)
	{
		_responsesRepository = responsesRepository;
	}

	public async Task<List<ResponseModel>> GetResponsesForOrderAsync(long orderId)
	{
		var searchParams = new ResponseSearchParams
		{
			OrderId = orderId
		};

		return await _responsesRepository.GetAsync(searchParams);
	}

	public async Task<List<ResponseModel>> GetResponsesForConsumerAsync(long consumerId)
	{
		var searchParams = new ResponseSearchParams
		{
			ConsumerId = consumerId
		};

		return await _responsesRepository.GetAsync(searchParams);
	}

	public async Task CreateResponseAsync(CreateResponseData createData)
	{
		var responseModel = new ResponseModel
		{
			ConsumerId = createData.ConsumerId,
			OrderId = createData.OrderId,
			ReduceData = createData.ReduceData
		};

		await _responsesRepository.SaveAsync(responseModel);
	}

	public async Task DeleteResponseAsync(long responseId)
	{
		var response = await _responsesRepository.GetByIdAsync(responseId);

		await _responsesRepository.DeleteAsync(response);
	}
}