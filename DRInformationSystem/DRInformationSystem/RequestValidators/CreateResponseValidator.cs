using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class CreateResponseValidator : AbstractValidator<CreateResponseDto>
{
	public CreateResponseValidator()
	{
		RuleFor(x => x.ReduceData)
			.NotEmpty();

		RuleFor(x => x.ConsumerId)
			.NotEmpty();

		RuleFor(x => x.OrderId)
			.NotEmpty();

		RuleFor(x => x.ReduceData)
			.Must(x => x.Count == 24)
			.WithMessage("ReduceData must contains 24 element");
	}
}