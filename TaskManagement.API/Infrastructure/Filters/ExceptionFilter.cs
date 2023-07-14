using Hangfire.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;

namespace TaskManagement.API.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            HttpStatusCode statusCode = GetStatusCodeByException(exceptionType);

            HttpResponse response = context.HttpContext.Response;
            var result = JsonConvert.SerializeObject(new
            {
                isError = true,
                errorMessage = context.Exception.Message,
                errorCode = (int)statusCode,
            });
            response.WriteAsync(result);
        }
        private HttpStatusCode GetStatusCodeByException(Type exceptionType)
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