using System.Text.Json.Serialization;

namespace DRInformationSystem.Models.Validation;

public class ValidationFailureResponse
{
	[JsonIgnore(Condition = JsonIgnoreCondition.Never)]
	public string ErrorMessage { get; set; }
		
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string PropertyName { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public object AttemptedValue { get; set; }
}