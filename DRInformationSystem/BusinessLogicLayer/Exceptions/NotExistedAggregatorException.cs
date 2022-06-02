namespace BusinessLogicLayer.Exceptions;

public class NotExistedAggregatorException : Exception
{
	private const string ErrorMessage = "Aggregator with id {0} not exist";

	public NotExistedAggregatorException(long aggregatorId) : base(string.Format(ErrorMessage, aggregatorId))
	{
	}
}