using System.Collections.Generic;
using DRInformationSystem.DTO;
using DRInformationSystem.RequestValidators;
using NUnit.Framework;

namespace Tests.ValidatorTests;

[TestFixture]
public class CreateResponseValidatorTest
{
	private CreateResponseValidator _createResponseValidator;

	[SetUp]
	public void SetUpMethod()
	{
		_createResponseValidator = new CreateResponseValidator();
	}

	[TearDown]
	public void TearDownMethod()
	{
		_createResponseValidator = null;
	}

	[Test]
	public void Validate_WhenAllParametersEmpty_ReturnFalse()
	{
		var createResponseDto = new CreateResponseDto
		{
			ReduceData = new List<long>()
		};

		var actualValidationResult = _createResponseValidator.Validate(createResponseDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}

	[Test]
	public void Validate_WhenReduceDataContains20Element_ReturnFalse()
	{
		var createResponseDto = new CreateResponseDto
		{
			OrderId = 1,
			ConsumerId = 2,
			ReduceData = new List<long> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20}
		};

		var actualValidationResult = _createResponseValidator.Validate(createResponseDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}

	[Test]
	public void Validate_WhenAllRight_ReturnTrue()
	{
		var createResponseDto = new CreateResponseDto
		{
			OrderId = 1,
			ConsumerId = 2,
			ReduceData = new List<long> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20, 21,22,23,24}
		};

		var actualValidationResult = _createResponseValidator.Validate(createResponseDto);

		Assert.IsTrue(actualValidationResult.IsValid);
	}
}