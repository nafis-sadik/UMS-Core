using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Net;
using Services.Abstraction;
using System.Text;
using ActionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services;
using Repositories;
using System;

namespace Application.Helper
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute
    {
        private StringValues _userId, _token;
        private string UserId, Token;
        private string Salt;
        private ILogInService _logInService;
        public CustomAuthenticationAttribute()
        {
            _logInService = new LogInService(new UserInfoRepo(), new PassRepo());
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Request.Headers.TryGetValue("UserId", out _userId);
            filterContext.HttpContext.Request.Headers.TryGetValue("Token", out _token);

            UserId = _userId;
            Token = _token;

            if (string.IsNullOrEmpty(UserId))
                return;

            Salt = filterContext.HttpContext.Session.GetString(UserId);
            if (string.IsNullOrEmpty(Salt))
            {
                Salt = "ABC123abc!";
            }
            if (Salt == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Common", action = "NotLogedIn" }));
                return;
            }
            byte[] generatedToken = KeyDerivation.Pbkdf2(UserId, Encoding.Default.GetBytes(Salt), KeyDerivationPrf.HMACSHA1, 1000, 256);
            string generatedTokenStr = Convert.ToBase64String(generatedToken);
            if (Token != generatedTokenStr)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Common", action = "NotLogedIn" }));
                return;
            }
        }
    }
}
