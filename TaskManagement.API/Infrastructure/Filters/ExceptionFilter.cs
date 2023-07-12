using Hangfire.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;

namespace TaskManagement.API.Infrastructure.Filters
{
    public enum Exceptions
    {
        NullReferenceException = 1,
        FileNotFoundException = 2,
        OverflowException = 3,
        OutOfMemoryException = 4,
        InvalidCastException = 5,
        ObjectDisposedException = 6,
        UnauthorizedAccessException = 7,
        NotImplementedException = 8,
        NotSupportedException = 9,
        InvalidOperationException = 10,
        TimeoutException = 11,
        ArgumentException = 12,
        FormatException = 13,
        StackOverflowException = 14,
        SqlException = 15,
        IndexOutOfRangeException = 16,
        IOException = 17
    }
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = (context.Exception as WebException != null &&
                        ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                         ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                         : GetErrorCode(context.Exception.GetType());
            string errorMessage = context.Exception.Message;
            string customErrorMessage = "";
            string stackTrace = context.Exception.StackTrace;


            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new
            {
                message = customErrorMessage,
                isError = true,
                errorMessage = errorMessage,
                errorCode = statusCode,
                model = string.Empty
            });
        }

        private HttpStatusCode GetErrorCode(Type exceptionType)
        {
            Exceptions tryParseResult;
            if (Enum.TryParse<Exceptions>(exceptionType.Name, out tryParseResult))
            {
                switch (tryParseResult)
                {
                    case Exceptions.NullReferenceException: return HttpStatusCode.LengthRequired;
                    case Exceptions.FileNotFoundException: return HttpStatusCode.NotFound;
                    case Exceptions.OverflowException: return HttpStatusCode.RequestedRangeNotSatisfiable;
                    case Exceptions.OutOfMemoryException: return HttpStatusCode.ExpectationFailed;
                    case Exceptions.InvalidCastException: return HttpStatusCode.PreconditionFailed;
                    case Exceptions.ObjectDisposedException: return HttpStatusCode.Gone;
                    case Exceptions.UnauthorizedAccessException: return HttpStatusCode.Unauthorized;
                    case Exceptions.NotImplementedException: return HttpStatusCode.NotImplemented;
                    case Exceptions.NotSupportedException: return HttpStatusCode.NotAcceptable;
                    case Exceptions.InvalidOperationException: return HttpStatusCode.MethodNotAllowed;
                    case Exceptions.TimeoutException: return HttpStatusCode.RequestTimeout;
                    case Exceptions.ArgumentException: return HttpStatusCode.BadRequest;
                    case Exceptions.StackOverflowException: return HttpStatusCode.RequestedRangeNotSatisfiable;
                    case Exceptions.FormatException: return HttpStatusCode.UnsupportedMediaType;
                    case Exceptions.IOException: return HttpStatusCode.NotFound;
                    case Exceptions.IndexOutOfRangeException: return HttpStatusCode.ExpectationFailed;
                    default: return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}