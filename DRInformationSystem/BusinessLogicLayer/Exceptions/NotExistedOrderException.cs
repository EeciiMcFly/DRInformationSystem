namespace BusinessLogicLayer.Exceptions;

public class NotExistedOrderException : Exception
{
	private const string ErrorMessage = "Order with id {0} not exist";

	public NotExistedOrderException(long orderId) : base(string.Format(ErrorMessage, orderId))
	{
	}
}