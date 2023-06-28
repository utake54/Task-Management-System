using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        protected int UserId = 1;
        protected int CompanyId = 1;
        [ApiExplorerSettings(IgnoreApi = true)]
        public Dictionary<string, object> APIResponse(string msgCode, object result)
        {
            var response = new Dictionary<string, object>();
            response.Add("Message", msgCode);
            response.Add("Data", result);

            return response;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Dictionary<string, object> FailureResponse(string msgCode, object result)
        {
            var response = new Dictionary<string, object>();
            response.Add("Message", msgCode);
            response.Add("Data", result);

            return response;
        }
    }
}
