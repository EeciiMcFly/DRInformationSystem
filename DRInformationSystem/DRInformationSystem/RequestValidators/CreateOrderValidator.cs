using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
	public CreateOrderValidator()
	{
		RuleFor(x => x.PeriodStart)
			.NotEmpty();

		RuleFor(x => x.PeriodEnd)
			.NotEmpty();

		RuleFor(x => x.PeriodStart)
			.GreaterThanOrEqualTo(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0,
				0, 0, DateTimeKind.Utc).AddDays(1))
			.WithMessage($"Start of period must be greater than now more then 1 day. ");

		RuleFor(x => x.PeriodEnd)
			.GreaterThan(x => x.PeriodStart)
			.WithMessage("End of period must be greater than start");
	}
}