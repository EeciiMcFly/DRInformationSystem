using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class CompleteOrderValidator : AbstractValidator<CompleteOrderDto>
{
	public CompleteOrderValidator()
	{
		RuleFor(x => x.OrderId)
			.NotEmpty();

		RuleFor(x => x.ResponseIds)
			.NotEmpty();
	}
}