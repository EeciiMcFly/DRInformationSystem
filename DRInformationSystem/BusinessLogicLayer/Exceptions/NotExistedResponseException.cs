namespace BusinessLogicLayer.Exceptions;

public class NotExistedResponseException : Exception
{
	private const string ErrorMessage = "Response with id {0} not exist";

	public NotExistedResponseException(long consumerId) : base(string.Format(ErrorMessage, consumerId))
	{
		
	}
}