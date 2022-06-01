namespace BusinessLogicLayer.Exceptions;

public class EmptyResponsesListForCompleteOrderException : Exception
{
	private const string ErrorMessage = "Responses must be exist for compete order";

	public EmptyResponsesListForCompleteOrderException() : base(ErrorMessage)
	{
	}
}