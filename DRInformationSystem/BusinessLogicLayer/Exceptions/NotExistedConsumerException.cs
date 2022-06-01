namespace BusinessLogicLayer.Exceptions;

public class NotExistedConsumerException : Exception
{
	private const string ErrorMessage = "Consumer with id {0} not exist";

	public NotExistedConsumerException(long consumerId) : base(string.Format(ErrorMessage, consumerId))
	{
		
	}
}