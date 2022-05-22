using System.Net.Mail;
using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class CreateInviteValidator : AbstractValidator<CreateInviteDto>
{
	public CreateInviteValidator()
	{
		RuleFor(invite => invite.Email).NotNull()
			.WithMessage("Email address is required.");
		RuleFor(invite => invite.Email).Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
			.WithMessage("Email is not correct");
		RuleFor(invite => invite.Email).Must(x => MailAddress.TryCreate(x, out var mailAddress))
			.WithMessage("Email is not correct");
	}
}