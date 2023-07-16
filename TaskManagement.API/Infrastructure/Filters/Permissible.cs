using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Web.Http.Controllers;
using TaskManagement.Model.Model.ResponseModel;

namespace TaskManagement.API.Infrastructure.Filters
{
    public class Permissible : Attribute, IActionFilter
    {
        private readonly string _role;

        public Permissible(string role)
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
            string[] permissions = _role.Split(',');

            var tokenRole = context.HttpContext.Items["Role"].ToString();

            var hasPermissibleRole = permissions.Where(x => x == tokenRole).Any();

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

