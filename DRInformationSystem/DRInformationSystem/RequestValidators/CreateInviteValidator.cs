using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class CreateInviteValidator : AbstractValidator<CreateInviteDto>
{
	public CreateInviteValidator()
	{
		RuleFor(invite => invite.Email).NotNull()
			.WithMessage("Email address is required.");

		RuleFor(invite => invite.Email)
			.EmailAddress()
			.WithMessage("Email is not correct");
	}
}