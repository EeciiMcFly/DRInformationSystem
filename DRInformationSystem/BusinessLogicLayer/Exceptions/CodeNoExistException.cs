namespace BusinessLogicLayer.Exceptions;

public class CodeNoExistException : Exception
{
	private const string ErrorMessage = "Invite code {0} not exist";

	public CodeNoExistException(string inviteCode) : base(string.Format(ErrorMessage, inviteCode))
	{
	}
}