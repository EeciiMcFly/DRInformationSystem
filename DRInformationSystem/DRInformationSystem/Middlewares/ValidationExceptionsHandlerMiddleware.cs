using System.Net;
using System.Text.Json;
using AutoMapper;
using DRInformationSystem.Models.Validation;
using FluentValidation;

namespace DRInformationSystem.Middlewares;

public class ValidationExceptionsHandlerMiddleware
{
	private readonly RequestDelegate _next;
	private readonly IMapper _mapper;

	public ValidationExceptionsHandlerMiddleware(RequestDelegate next,
		IMapper mapper)
	{
		_next = next;
		_mapper = mapper;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next.Invoke(context);
		}
		catch (ValidationException validationException)
		{
			await HandleValidationExceptionAsync(context, validationException);
				
			throw;
		}
	}

	private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
	{
		var response = context.Response;
			
		response.ContentType = "application/json";
		response.StatusCode = (int)HttpStatusCode.BadRequest;

		var validationResponse = _mapper.Map<ValidationExceptionResponse>(exception);
			
		await response.WriteAsync(JsonSerializer.Serialize(validationResponse));
	}
}