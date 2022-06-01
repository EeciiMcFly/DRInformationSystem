using DRInformationSystem.DTO;
using FluentValidation;

namespace DRInformationSystem.RequestValidators;

public class UpdateSheddingValidator : AbstractValidator<UpdateSheddingDto>
{
	public UpdateSheddingValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();

		RuleFor(x => x.Status)
			.NotEmpty();
	}
}