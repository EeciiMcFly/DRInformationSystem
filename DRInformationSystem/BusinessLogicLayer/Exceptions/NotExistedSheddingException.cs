namespace BusinessLogicLayer.Exceptions;

public class NotExistedSheddingException : Exception
{
	private const string ErrorMessage = "Shedding with id {0} not exist";

	public NotExistedSheddingException(long consumerId) : base(string.Format(ErrorMessage, consumerId))
	{
	}
}