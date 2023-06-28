using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.Utility;

namespace TaskManagement.API.Infrastructure.Filters
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var model = new Dictionary<string, object>();
                var data = new List<string>();
                var errors = context.ModelState.Values.Select(x => x.Errors);

                foreach (var error in errors)
                {
                    data.AddRange(error.Select(x => x.ErrorMessage));
                }

                var response = new Dictionary<string, object>();
                response.Add(Constants.RESPONSE_MESSAGE_FIELD, "ModelSate is not valid");
                response.Add(Constants.RESPNSE_ERROR_FIELD, data);
                context.Result = new ObjectResult(response);
            }
        }
    }
}
