using BusinessLogicLayer.Utils;
using NUnit.Framework;

namespace Tests.UtilTests;

[TestFixture]
public class CodeGeneratorTest
{
	[TestCase("1000-1001", "1000-1002")]
	[TestCase("1000-9999", "1001-1001")]
	public void GenerateInviteCodeTest(string lastInviteCode, string expectedOutput)
	{
		var actualOutput = CodeGenerator.GenerateInviteCode(lastInviteCode);
		
		Assert.AreEqual(expectedOutput, actualOutput);
	}
}