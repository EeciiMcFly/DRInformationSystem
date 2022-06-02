using System;
using DRInformationSystem.DTO;
using DRInformationSystem.RequestValidators;
using NUnit.Framework;

namespace Tests.ValidatorTests;

[TestFixture]
public class CreateOrderValidatorTest
{
	private CreateOrderValidator _createOrderValidator;

	[SetUp]
	public void SetUpMethod()
	{
		_createOrderValidator = new CreateOrderValidator();
	}

	[TearDown]
	public void TearDownMethod()
	{
		_createOrderValidator = null;
	}
	
	[Test]
	public void Validate_WhenPeriodStartAndPeriodEndNull_ReturnFalse()
	{
		var createOrderDto = new CreateOrderDto();
		var actualValidationResult = _createOrderValidator.Validate(createOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}

	[Test]
	public void Validate_WhenPeriodStartNull_ReturnFalse()
	{
		var createOrderDto = new CreateOrderDto
		{
			PeriodEnd = DateTime.UtcNow.AddDays(10)
		};

		var actualValidationResult = _createOrderValidator.Validate(createOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}
	
	[Test]
	public void Validate_WhenPeriodEndNull_ReturnFalse()
	{
		var createOrderDto = new CreateOrderDto
		{
			PeriodStart = DateTime.UtcNow.AddDays(10)
		};

		var actualValidationResult = _createOrderValidator.Validate(createOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}

	[Test]
	public void Validate_WhenPeriodEndLessThanPeriodStart_ReturnFalse()
	{
		var createOrderDto = new CreateOrderDto
		{
			PeriodStart = DateTime.UtcNow.AddDays(10),
			PeriodEnd = DateTime.UtcNow.AddDays(9)
		};

		var actualValidationResult = _createOrderValidator.Validate(createOrderDto);

		Assert.IsFalse(actualValidationResult.IsValid);
	}

	[Test]
	public void Validate_WhenAllRight_ReturnTrue()
	{
		var createOrderDto = new CreateOrderDto
		{
			PeriodStart = DateTime.UtcNow.AddDays(9),
			PeriodEnd = DateTime.UtcNow.AddDays(10)
		};

		var actualValidationResult = _createOrderValidator.Validate(createOrderDto);

		Assert.IsTrue(actualValidationResult.IsValid);
	}
}