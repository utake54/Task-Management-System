using Newtonsoft.Json;
using System.Net;

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
            switch (exceptionType.Name)
            {
                case nameof(NullReferenceException): return HttpStatusCode.NotImplemented;
                case nameof(FileNotFoundException): return HttpStatusCode.NotFound;
                case nameof(OverflowException): return HttpStatusCode.RequestedRangeNotSatisfiable;
                case nameof(OutOfMemoryException): return HttpStatusCode.ExpectationFailed;
                case nameof(TimeoutException): return HttpStatusCode.RequestTimeout;
                case nameof(IndexOutOfRangeException): return HttpStatusCode.ExpectationFailed;

                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}