using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int UserId => int.Parse(this.User.Claims.First(x => x.Type == "UserId").Value);
        protected int RoleId => int.Parse(this.User.Claims.First(x => x.Type == "RoleId").Value);
        protected int CompanyId => int.Parse(this.User.Claims.First(x => x.Type == "CompanyId").Value);


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
            response.Add("Data", msgCode);
            response.Add("Failed", result);

            return response;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Dictionary<string, object> UnauthorizeResponse(string msgCode, object result)
        {
            var response = new Dictionary<string, object>();
            response.Add("Message", "Invalid Credential");
            response.Add("Unauthorized", msgCode);

            return response;
        }
    }
}
