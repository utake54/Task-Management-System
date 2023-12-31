﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Utility;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int UserId => int.Parse(this.User.Claims.First(x => x.Type == "UserId").Value);
        protected int RoleId => int.Parse(this.User.Claims.First(x => x.Type == "RoleId").Value);
        protected int CompanyId => int.Parse(this.User.Claims.First(x => x.Type == "CompanyId").Value);


        protected Dictionary<string, object> APIResponse(bool result, string failureMsg, string successMessage)
        {
            var response = new Dictionary<string, object>
            {
                { "Data", result },
                { result == true ? "Message" : "Error", ContentLoader.ReturnMessage(result == true ? successMessage : failureMsg) }
            };
            return response;
        }


        protected Dictionary<string, object> APIResponse(object result, string failureMsg, string successMessage)
        {
            var response = new Dictionary<string, object>
            {
                { "Data", result }
            };
            if (string.IsNullOrEmpty(failureMsg)|| failureMsg =="Success" )
            {
                response.Add(Constants.RESPONSE_MESSAGE_FIELD, ContentLoader.ReturnMessage(successMessage));

            }
            else
            {
                response.Add(Constants.RESPONSE_MESSAGE_FIELD, ContentLoader.ReturnMessage(failureMsg));

            }
            return response;
        }


        protected Dictionary<string, object> APIResponse(object result, string msgCode = "")
        {
            var response = new Dictionary<string, object>()
            {
                {"Data",result }
            };
            if (!string.IsNullOrEmpty(msgCode))
            {
                response.Add(Constants.RESPONSE_MESSAGE_FIELD, ContentLoader.ReturnMessage(msgCode));
            }
            return response;
        }


        //protected Dictionary<string, object> UnauthorizeResponse(string msgCode, object result)
        //{
        //    var response = new Dictionary<string, object>
        //    {
        //        { "Message", ContentLoader.ReturnMessage("TM003") },
        //        { "Unauthorized", msgCode }
        //    };

        //    return response;
        //}
    }
}
