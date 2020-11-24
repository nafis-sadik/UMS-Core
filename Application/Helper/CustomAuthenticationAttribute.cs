using Microsoft.AspNetCore.Mvc.Filters;
using ActionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute;

namespace Application.Helper
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
