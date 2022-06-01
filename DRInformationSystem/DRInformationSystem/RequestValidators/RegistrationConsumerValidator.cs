using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class RegistrationConsumerValidator : AbstractValidator<RegistrationConsumerDto>
{
	public RegistrationConsumerValidator()
	{
		RuleFor(x => x.Login)
			.NotEmpty();

		RuleFor(x => x.Login)
			.EmailAddress()
			.WithMessage("Login is not match with email form");

		RuleFor(x => x.Password)
			.NotEmpty();

		RuleFor(x => x.PasswordConfirm)
			.NotEmpty();

		RuleFor(x => x.Password)
			.Equal(dto => dto.PasswordConfirm)
			.WithMessage("Password and password confirm must be equal");

		RuleFor(x => x.InviteCode)
			.NotEmpty();

		RuleFor(x => x.InviteCode)
			.Matches(@"\b\d{4}-\d{4}\b")
			.WithMessage("InviteCode is not correct");
	}
}