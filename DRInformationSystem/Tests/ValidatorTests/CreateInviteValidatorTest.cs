using DRInformationSystem.DTO;
using DRInformationSystem.RequestValidators;
using NUnit.Framework;

namespace Tests.ValidatorTests;

[TestFixture]
public class CreateInviteValidatorTest
{
	private CreateInviteValidator _createInviteValidator;
	
	[SetUp]
	public void SetUpMethod()
	{
		_createInviteValidator = new CreateInviteValidator();
	}

	[TearDown]
	public void TearDownMethod()
	{
		_createInviteValidator = null;
	}

	[Test]
	public void Validate_WhenEmailIsNull_ReturnFalse()
	{
		var createInviteDto = new CreateInviteDto();

		var actualValidationResult = _createInviteValidator.Validate(createInviteDto);
		
		Assert.IsFalse(actualValidationResult.IsValid);
	}
	
	[Test]
	public void Validate_WhenEmailNoEmailForm_ReturnFalse()
	{
		var createInviteDto = new CreateInviteDto
		{
			Email = "default"
		};

		var actualValidationResult = _createInviteValidator.Validate(createInviteDto);
		
		Assert.IsFalse(actualValidationResult.IsValid);
	}
	
	[Test]
	public void Validate_WhenEmailCorrect_ReturnTrue()
	{
		var createInviteDto = new CreateInviteDto
		{
			Email = "test@examp.com"
		};

		var actualValidationResult = _createInviteValidator.Validate(createInviteDto);
		
		Assert.IsTrue(actualValidationResult.IsValid);
	}
}