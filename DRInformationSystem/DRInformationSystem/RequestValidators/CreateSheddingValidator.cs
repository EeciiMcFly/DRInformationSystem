using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class CreateSheddingValidator : AbstractValidator<CreateSheddingDto>
{
	public CreateSheddingValidator()
	{
		RuleFor(x => x.StartTime)
			.NotEmpty();

		RuleFor(x => x.StartTime)
			.GreaterThanOrEqualTo(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0,
				0, 0, DateTimeKind.Utc).AddDays(1))
			.WithMessage($"Start of period must be greater than now more then 1 day. ");

		RuleFor(x => x.Duration)
			.NotEmpty();

		RuleFor(x => x.Duration)
			.Must(x => x.Equals(2) || x.Equals(4))
			.WithMessage("Duration value must be 2 or 4");

		RuleFor(x => x.Volume)
			.NotEmpty();

		RuleFor(x => x.OrderId)
			.NotEmpty();

		RuleFor(x => x.ConsumerId)
			.NotEmpty();
	}
}