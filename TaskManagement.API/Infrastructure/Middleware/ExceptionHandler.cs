using Newtonsoft.Json;
using System.Net;
using TaskManagement.Utility.ExceptionHelper;

namespace TaskManagement.API.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            HttpStatusCode statusCode = GetStatusCodeByException(exceptionType);

            HttpResponse response = context.Response;
            var result = JsonConvert.SerializeObject(new
            {
                isError = true,
                errorMessage = exception.Message,
                errorCode = (int)statusCode,
            });

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            response.ContentLength = result.Length;
            return response.WriteAsync(result);
        }

        private static HttpStatusCode GetStatusCodeByException(Type exceptionType)
        {
            return exceptionType.Name switch
            {
                nameof(NullReferenceException) => HttpStatusCode.NotImplemented,
                nameof(FileNotFoundException) => HttpStatusCode.NotFound,
                nameof(OverflowException) => HttpStatusCode.RequestedRangeNotSatisfiable,
                nameof(OutOfMemoryException) => HttpStatusCode.ExpectationFailed,
                nameof(TimeoutException) => HttpStatusCode.RequestTimeout,
                nameof(IndexOutOfRangeException) => HttpStatusCode.ExpectationFailed,
                nameof(RecordNotFoundException) => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}