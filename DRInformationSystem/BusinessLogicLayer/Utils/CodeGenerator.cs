namespace BusinessLogicLayer.Utils;

public static class CodeGenerator
{
	public static string GenerateInviteCode(string lastInviteCodeString)
	{
		var firstPart = Convert.ToInt32(lastInviteCodeString.Split("-")[0]);
		var secondPart = Convert.ToInt32(lastInviteCodeString.Split("-")[1]);
		if (secondPart == 9999)
		{
			secondPart = 1001;
			firstPart += 1;
		}
		else
		{
			secondPart += 1;
		}

		var code = $"{firstPart}-{secondPart}";
		return code;
	}
}