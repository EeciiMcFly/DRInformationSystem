using BusinessLogicLayer.Utils;
using NUnit.Framework;

namespace Tests.UtilTests;

[TestFixture]
public class StringHasherTest
{
	[TestCase("12345", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5")]
	[TestCase("teststring", "3c8727e019a42b444667a587b6001251becadabbb36bfed8087a92c18882d111")]
	public void GetSha256Hash_ReturnHashString(string input, string expectedOutput)
	{
		var actualOutput = StringHasher.GetSha256Hash(input);

		Assert.AreEqual(expectedOutput, actualOutput);
	}
}