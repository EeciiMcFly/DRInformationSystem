using DataAccessLayer.Models;

namespace BusinessLogicLayer.Exceptions;

public class NoValidNewStateForSheddingException : Exception
{
	private const string ErrorMessage = "Shedding can not change status from {0} to {1}";

	public NoValidNewStateForSheddingException(SheddingState fromState, SheddingState toState) 
		: base(string.Format(ErrorMessage, fromState, toState))
	{
		
	}
}