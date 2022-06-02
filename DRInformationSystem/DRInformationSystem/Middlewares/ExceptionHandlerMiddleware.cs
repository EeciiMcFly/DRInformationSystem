using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using BusinessLogicLayer.Exceptions;
using DRInformationSystem.Models;

namespace DRInformationSystem.Middlewares;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next.Invoke(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionMessageAsync(context, ex);

			throw;
		}
	}

	private async Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
	{
		var response = context.Response;

		var exceptionResponse = MapExceptionToStatusCodeAndMessage(exception);

		response.ContentType = "application/json";
		response.StatusCode = exceptionResponse.StatusCode;

		await response.WriteAsync(JsonSerializer.Serialize(exceptionResponse));
	}

	private ErrorResponse MapExceptionToStatusCodeAndMessage(Exception ex) => ex switch
	{
		BadAuthException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.Unauthorized,
			Message = "Invalid login or password.",
		},
		AlreadyUsedLoginException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		AlreadyActivatedInviteCodeException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		CodeNoExistException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		NotExistedOrderException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		EmptyResponsesListForCompleteOrderException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		NotExistedConsumerException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		NotExistedResponseException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		NotExistedSheddingException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		NoValidNewStateForSheddingException => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.BadRequest,
			Message = ex.Message
		},
		_ => new ErrorResponse
		{
			StatusCode = (int) HttpStatusCode.InternalServerError,
			Message = "Internal server error.",
		},
	};
}