using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pronia.Attributs
{

    public class IsAuthenticated : ActionFilterAttribute, IActionFilter
    {
       
      
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userService = filterContext.HttpContext.RequestServices.GetRequiredService<IUserService>();
            if (userService.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "account" },
                    { "action", "dashboard" }
                });
            }
        }
    }
}
