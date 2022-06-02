using System;
using DRInformationSystem.DTO;
using DRInformationSystem.RequestValidators;
using NUnit.Framework;

namespace Tests.ValidatorTests;

[TestFixture]
public class CreateSheddingValidatorTest
{
	private CreateSheddingValidator _createSheddingValidator;
	
	[SetUp]
	public void SetUpMethod()
	{
		_createSheddingValidator = new CreateSheddingValidator();
	}

	[TearDown]
	public void TearDownMethod()
	{
		_createSheddingValidator = null;
	}

	[Test]
	public void Validate_WhenAllParametersEmpty_ReturnFalse()
	{
		var createSheddingDto = new CreateSheddingDto();

		var actualValidateResult = _createSheddingValidator.Validate(createSheddingDto);
		
		Assert.IsFalse(actualValidateResult.IsValid);
	}

	[TestCase(0, false)]
	[TestCase(1, false)]
	[TestCase(2, true)]
	[TestCase(4, true)]
	public void Validate_WhenDurationHasValue_ReturnExpectedValue(int durationValue, bool expectedValue)
	{
		var createSheddingDto = new CreateSheddingDto
		{
			Duration = durationValue,
			Volume = 1,
			ConsumerId = 2,
			OrderId = 3,
			StartTime = DateTime.UtcNow.AddDays(10)
		};
		
		var actualValidateResult = _createSheddingValidator.Validate(createSheddingDto);
		
		Assert.AreEqual(expectedValue, actualValidateResult.IsValid);
	}
}