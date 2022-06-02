using System.Collections.Generic;
using DRInformationSystem.DTO;
using DRInformationSystem.RequestValidators;
using NUnit.Framework;

namespace Tests.ValidatorTests;

[TestFixture]
public class CompleteOrderValidatorTest
{
	private CompleteOrderValidator _completeOrderValidator;
	
	[SetUp]
	public void SetUpMethod()
	{
		_completeOrderValidator = new CompleteOrderValidator();
	}

	[TearDown]
	public void TearDownMethod()
	{
		_completeOrderValidator = null;
	}
	
	[Test]
	public void Validate_WhenOrderIdAndResponsesIdsNull_ReturnFalse()
	{
		var completeOrderDto = new CompleteOrderDto();
		var actualValidationResult = _completeOrderValidator.Validate(completeOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}

	[Test]
	public void Validate_WhenOrderIdNull_ReturnFalse()
	{
		var completeOrderDto = new CompleteOrderDto
		{
			ResponseIds = new List<long>()
		};
		var actualValidationResult = _completeOrderValidator.Validate(completeOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}
	
	[Test]
	public void Validate_WhenResponseIdsNull_ReturnFalse()
	{
		var completeOrderDto = new CompleteOrderDto
		{
			OrderId = 5
		};
		var actualValidationResult = _completeOrderValidator.Validate(completeOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}
	
	[Test]
	public void Validate_WhenResponseIdsEmpty_ReturnFalse()
	{
		var completeOrderDto = new CompleteOrderDto
		{
			OrderId = 5,
			ResponseIds = new List<long>()
		};
		var actualValidationResult = _completeOrderValidator.Validate(completeOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}

	[Test]
	public void Validate_WhenOrderIdExistAndResponseIdsExist_ReturnTrue()
	{
		var completeOrderDto = new CompleteOrderDto
		{
			OrderId = 5,
			ResponseIds = new List<long> {1,2,4,5,6}
		};
		var actualValidationResult = _completeOrderValidator.Validate(completeOrderDto);

		Assert.IsTrue(actualValidationResult.IsValid);
	}
}