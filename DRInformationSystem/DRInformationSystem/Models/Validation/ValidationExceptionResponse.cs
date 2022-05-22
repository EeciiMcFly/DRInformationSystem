namespace DRInformationSystem.Models.Validation;

public class ValidationExceptionResponse
{
	public IEnumerable<ValidationFailureResponse> Errors { get; set; }
}