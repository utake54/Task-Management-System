using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Web.Http.Controllers;
using TaskManagement.Model.Model.ResponseModel;

namespace TaskManagement.API.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class Permissible : Attribute, IActionFilter
    {
        private readonly string[] _role;

        public Permissible(params string[] role)
        {
            _role = role;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenRole = context.HttpContext.Items["Role"].ToString();

            var hasPermissibleRole = this._role.Contains(tokenRole);
            if (!hasPermissibleRole)
            {
                context.Result = new UnauthorizedResult();
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}

