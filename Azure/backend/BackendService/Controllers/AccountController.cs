// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Authentication;
    using Microsoft.AspNet.Mvc;
    using Backend;
    using System.Threading.Tasks;

    //[RequireHttps]
    [Route("/[controller]")]
    public class AccountController : Controller //IdentityController<IdentityUser, IdentityDbContext>
    {
        public AccountRepository Repository { get; set; }

        public AccountController()
        {
            Repository = new AccountRepository();
        }

        [HttpGet("~/signin")]
        public ActionResult SignIn(string returnUrl = null)
        {
            // Note: the "returnUrl" parameter corresponds to the endpoint the user agent
            // will be redirected to after a successful authentication and not
            // the redirect_uri of the requesting client application.
            ViewBag.ReturnUrl = returnUrl;

            // Note: in a real world application, you'd probably prefer creating a specific view model.
            ActionContext.HttpContext.GetExternalProviders();

            return View("SignIn");
        }

        [HttpPost("~/signin")]
        public ActionResult SignIn(string provider, string returnUrl)
        {
            // Note: the "provider" parameter corresponds to the external
            // authentication provider choosen by the user agent.
            if (string.IsNullOrEmpty(provider))
            {
                return HttpBadRequest();
            }

            if (!ActionContext.HttpContext.IsProviderSupported(provider))
            {
                return HttpBadRequest();
            }

            // Note: the "returnUrl" parameter corresponds to the endpoint the user agent
            // will be redirected to after a successful authentication and not
            // the redirect_uri of the requesting client application.
            if (string.IsNullOrEmpty(returnUrl))
            {
                return HttpBadRequest();
            }

            // Instruct the middleware corresponding to the requested external identity
            // provider to redirect the user agent to its own authorization endpoint.
            // Note: the authenticationScheme parameter must match the value configured in Startup.cs
            return new ChallengeResult(provider, new AuthenticationProperties
            {
                RedirectUri = returnUrl
            });
        }

        [HttpGet("~/signout"), HttpPost("~/signout")]
        public void SignOut()
        {
            // Instruct the cookies middleware to delete the local cookie created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).

            ActionContext.HttpContext.Authentication.SignOut("ServerCookie");
        }
    }
}

namespace Backend
{
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Authentication;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class HttpContextExtensions
    {
        public static IEnumerable<AuthenticationDescription> GetExternalProviders(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return from description in context.Authentication.GetAuthenticationSchemes()
                   where !string.IsNullOrEmpty(description.AuthenticationScheme)
                   select description;
        }

        public static bool IsProviderSupported(this HttpContext context, string provider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return (from description in context.GetExternalProviders()
                    where string.Equals(description.AuthenticationScheme, provider, StringComparison.OrdinalIgnoreCase)
                    select description).Any();
        }
    }
}