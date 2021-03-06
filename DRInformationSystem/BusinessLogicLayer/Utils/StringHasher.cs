using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicLayer.Utils;

public static class StringHasher
{
	public static string GetSha256Hash(string input)
	{
		var inputBytes = Encoding.UTF8.GetBytes(input);

		var stringBuilder = new StringBuilder();
		using (var hasher = SHA256.Create())
		{
			var resultBytes = hasher.ComputeHash(inputBytes);

			foreach (var resultByte in resultBytes) 
				stringBuilder.Append(resultByte.ToString("x2"));
		}

		return stringBuilder.ToString();
	}
}