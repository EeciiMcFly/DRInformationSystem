using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class ResponsesService : IResponsesService
{
	private readonly IResponsesRepository _responsesRepository;
	private readonly IOrdersRepository _ordersRepository;
	private readonly IConsumersRepository _consumersRepository;

	public ResponsesService(IResponsesRepository responsesRepository,
		IOrdersRepository ordersRepository,
		IConsumersRepository consumersRepository)
	{
		_responsesRepository = responsesRepository;
		_ordersRepository = ordersRepository;
		_consumersRepository = consumersRepository;
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
		var order = await _ordersRepository.GetByIdAsync(createData.OrderId);
		if (order == null)
			throw new NotExistedOrderException(createData.OrderId);

		var consumer = await _consumersRepository.GetByIdAsync(createData.ConsumerId);
		if (consumer == null)
			throw new NotExistedConsumerException(createData.ConsumerId);

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
		if (response == null)
			throw new NotExistedResponseException(responseId);

		await _responsesRepository.DeleteAsync(response);
	}
}