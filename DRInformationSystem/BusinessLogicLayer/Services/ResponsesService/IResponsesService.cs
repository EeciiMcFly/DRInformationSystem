using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services;

public interface IResponsesService
{
	Task<List<ResponseModel>> GetResponsesForOrderAsync(long orderId);
	Task<List<ResponseModel>> GetResponsesForConsumerAsync(long consumerId);
	Task CreateResponseAsync(CreateResponseData createData);
	Task DeleteResponseAsync(long responseId);
}