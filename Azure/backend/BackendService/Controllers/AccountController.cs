// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    using Microsoft.AspNet.Http.Authentication;
    using Microsoft.AspNet.Mvc;
    using System.Linq;
    using System.Security.Claims;

    //[RequireHttps]
    [Route("/[controller]")]
    public class AccountController : Controller //IdentityController<IdentityUser, IdentityDbContext>
    {
        public AccountRepository Repository { get; set; }

        public AccountController()
        {
            Repository = new AccountRepository();
        }

        [HttpPost]
        public bool Login()
        {
            return true;
        }

        // GET api/values/5
        [HttpPost]
        public IActionResult SignOut()
        {
            return Index();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return true;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var provider = "Cookies";
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("Callback", new { provider }) //ExternalLoginCallback/Login
            };

            return new ChallengeResult(provider, properties);

        }

        public ActionResult Callback(string provider)
        {
            var ctx = Request.HttpContext;
            var result = ctx.Authentication.AuthenticateAsync("ExternalCookie").Result;
            ctx.Authentication.SignOut("ExternalCookie");

            var claims = result.Principal.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, provider));

            var ci = new ClaimsIdentity(claims, "Cookie");
            ctx.Authentication.SignIn(ctx.Authentication.GetAuthenticationSchemes().First().AuthenticationScheme, result.Principal);

            return Redirect("~/");
        }

        // https://api.twitter.com/oauth/authenticate?oauth_token=TcZ4H3Rg92YKwlTKDsestfO8S

        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Twitter(string returnUrl) //
        {
            return RedirectToRoute("Home/Index");
        }

        [HttpGet]
        public IActionResult SignIn(string param)
        {
            return View();
        }

        public ActionResult ExternalLogin(string provider)
        {
            /*var ctx = Request.HttpContext;
            ctx.Authentication.Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = provider
                },
                provider);*/
            return new HttpUnauthorizedResult();
        }

        /*
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await Microsoft.AspNet.Identity.SignInManager AuthenticationManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction("SignIn");

            var identity = GetBasicUserIdentity(info.DefaultUserName);
            var properties = new AuthenticationProperties { IsPersistent = true };

            //AuthenticationManager.SignIn(properties "", ClaimsPrincipal.Current, properties);

            return Redirect(returnUrl);
        }

        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }


        protected ClaimsIdentity GetBasicUserIdentity(string name)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };

            return new ClaimsIdentity(claims); / DefaultAuthenticationTypes.ApplicationCookie
        }*/
    }
}