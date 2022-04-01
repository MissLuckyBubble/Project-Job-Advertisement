using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SilviqDancheva_2101321099.Entities;
using SilviqDancheva_2101321099.ExtentionMethods;
using System;
namespace SilviqDancheva_2101321099.ActionFilters
{
    public class AuthFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetObject<User>("loggedUser") == null)
            {
                context.Result = new RedirectResult("/Home/Index");
            }
        }
    }
    public class RoleFilterAttribue: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetObject<User>("loggedUser").RoleId!=1)
            {
                context.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}