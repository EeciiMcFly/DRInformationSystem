namespace BusinessLogicLayer.Exceptions;

public class AlreadyActivatedInviteCodeException : Exception
{
	private const string ErrorMessage = "Invite code {0} is already activated";

	public AlreadyActivatedInviteCodeException(string inviteCode) : base(string.Format(ErrorMessage, inviteCode))
	{
	}
}