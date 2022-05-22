using AutoMapper;
using DRInformationSystem.Models.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace DRInformationSystem.AutoMapperProfiles;

public class ValidationErrorsMappingProfile : Profile
{
	public ValidationErrorsMappingProfile()
	{
		CreateMap<ValidationFailure, ValidationFailureResponse>();
		CreateMap<ValidationException, ValidationExceptionResponse>();
	}
}