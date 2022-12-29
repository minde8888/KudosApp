
using Kudos.Domain.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Kudos.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ApiExceptionResponse error;

            switch (exception)
            {
                case KudoNotAllowedException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    error = new ApiExceptionResponse()
                    {
                        Reason = context.Response.StatusCode.ToString(),
                        Message = badRequestException.Message
                    };
                    break;

                case KudoResponseException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    error = new ApiExceptionResponse()
                    {
                        Reason = context.Response.StatusCode.ToString(),
                        Message = badRequestException.Message
                    };
                    break;

                case EmployeeNotFoundException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    error = new ApiExceptionResponse()
                    {
                        Reason = context.Response.StatusCode.ToString(),
                        Message = badRequestException.Message
                    };
                    break;

                case KudoReasonEnumNotFoundException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    error = new ApiExceptionResponse()
                    {
                        Reason = context.Response.StatusCode.ToString(),
                        Message = badRequestException.Message
                    };
                    break;

                case KudoReasonDescriptionNotFoundException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    error = new ApiExceptionResponse()
                    {
                        Reason = context.Response.StatusCode.ToString(),
                        Message = badRequestException.Message
                    };
                    break;

                case KudoExistException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    error = new ApiExceptionResponse()
                    {
                        Reason = context.Response.StatusCode.ToString(),
                        Message = badRequestException.Message
                    };
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    error = new ApiExceptionResponse()
                    {
                        Reason = "InternalServerError",
                        Message = "Internal server error occurred." + exception
                    };
                    break;
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
