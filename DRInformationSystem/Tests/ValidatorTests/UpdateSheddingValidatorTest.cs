using DRInformationSystem.DTO;
using DRInformationSystem.RequestValidators;
using NUnit.Framework;

namespace Tests.ValidatorTests;

[TestFixture]
public class UpdateSheddingValidatorTest
{
	private UpdateSheddingValidator _updateSheddingValidator;

	[SetUp]
	public void SetUpMethod()
	{
		_updateSheddingValidator = new UpdateSheddingValidator();
	}

	[TearDown]
	public void TearDownMethod()
	{
		_updateSheddingValidator = null;
	}

	[Test]
	public void Validate_WhenAllParametersEmpty_ReturnFalse()
	{
		var updateSheddingDto = new UpdateSheddingDto();

		var actualValidationResult = _updateSheddingValidator.Validate(updateSheddingDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}
}