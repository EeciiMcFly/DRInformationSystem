using DRInformationSystem.DTO;
using DRInformationSystem.RequestValidators;
using NUnit.Framework;

namespace Tests.ValidatorTests;

[TestFixture]
public class RegistrationConsumerValidatorTest
{
	private RegistrationConsumerValidator _registrationConsumerValidator;

	[SetUp]
	public void SetUpMethod()
	{
		_registrationConsumerValidator = new RegistrationConsumerValidator();
	}

	[TearDown]
	public void TearDownMethod()
	{
		_registrationConsumerValidator = null;
	}

	[Test]
	public void Validate_WhenLoginNoEmailForm_ReturnFalse()
	{
		var registrationConsumerDto = new RegistrationConsumerDto
		{
			Login = "login",
			Password = "pass",
			PasswordConfirm = "pass",
			InviteCode = "1000-1000"
		};

		var actualValidationResult = _registrationConsumerValidator.Validate(registrationConsumerDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}
}