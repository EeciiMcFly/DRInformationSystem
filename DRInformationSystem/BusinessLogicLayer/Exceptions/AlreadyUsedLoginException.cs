namespace BusinessLogicLayer.Exceptions;

public class AlreadyUsedLoginException : Exception
{
	private const string ErrorMessage = "Login {0} already used by other consumer";

	public AlreadyUsedLoginException(string login) : base(string.Format(ErrorMessage, login))
	{
	}
}