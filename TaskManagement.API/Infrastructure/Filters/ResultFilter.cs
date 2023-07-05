using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Utility;
using System.Net;

namespace TaskManagement.API.Infrastructure.Filters
{
    public class ResultFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            if (context.Result.GetType() == typeof(FileContentResult))
                return;

            var result = context.Result;
            var responseObj = new ResponseModel
            {
                Message = "Success",
                StatusCode = 200,
                Errors = null,
            };

            switch (result)
            {
                case OkObjectResult okresult:
                    responseObj.Data = okresult.Value;
                    break;
                case ObjectResult objectResult:
                    Dictionary<string, object>? data = (Dictionary<string, object>)objectResult.Value;
                    switch (data.Keys.Select(x => x).LastOrDefault())
                    {
                        case "Data":
                            responseObj.StatusCode = (int)HttpStatusCode.OK;
                            responseObj.Message = Convert.ToString(data[Constants.RESPONSE_MESSAGE_FIELD]);
                            responseObj.Data = data[Constants.RESPONSE_DATA_FIELD];
                            break;

                        case "Failed":
                            responseObj.StatusCode = (int)HttpStatusCode.NoContent;
                            responseObj.Message = Convert.ToString(data[Constants.RESPNSE_FAILURE_FIELD]);
                            responseObj.Data = data[Constants.RESPONSE_DATA_FIELD];
                            break;

                        case "Unauthorized":
                            responseObj.StatusCode = (int)HttpStatusCode.Unauthorized;
                            responseObj.Message = Convert.ToString(data[Constants.RESPONSE_MESSAGE_FIELD]);
                            //var errors = new Errors { Erros = (string)data[Constants.UNAUTHORIZED_RESPONSE_FIELD] };
                            break;
                    }
                    //responseObj.Message = data.ContainsKey(Constants.RESPONSE_MESSAGE_FIELD) ? Convert.ToString(data[Constants.RESPONSE_MESSAGE_FIELD]) : null;
                    //responseObj.Data = data.ContainsKey(Constants.RESPONSE_DATA_FIELD) ? data[Constants.RESPONSE_DATA_FIELD] : null;
                    break;
                case JsonResult json:
                    responseObj.Data = json.Value;
                    break;
                case OkResult _:
                case EmptyResult _:
                    responseObj.Data = null;
                    break;
                default:
                    responseObj.Data = result;
                    break;
            }

            context.Result = new JsonResult(responseObj);
        }
    }
}


//switch (data.Keys.Select(x => x).LastOrDefault())
//{
//    case "Data":
//        responseObj.StatusCode = (int)HttpStatusCode.OK;
//        responseObj.Message = Convert.ToString(data[Constants.RESPONSE_MESSAGE_FIELD]);
//        responseObj.Data = data[Constants.RESPONSE_DATA_FIELD];
//        break;

//    case "Failure":
//        responseObj.StatusCode = (int)HttpStatusCode.PreconditionFailed;
//        responseObj.Message = Convert.ToString(data[Constants.RESPONSE_MESSAGE_FIELD]);
//        error = new Errors { Erros = (string)data[Constants.RESPNSE_ERROR_FIELD] };
//        //responseObj.Errors = error;
//        break;

//    case "Unauthorized":
//        responseObj.StatusCode = (int)HttpStatusCode.Unauthorized;
//        responseObj.Message = Convert.ToString(data[Constants.RESPONSE_MESSAGE_FIELD]);
//        error = new Errors { Erros = (string)data[Constants.UNAUTHORIZED_RESPONSE_FIELD] };
//        //responseObj.Errors = error;
//        break;
//}