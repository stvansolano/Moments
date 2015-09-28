namespace BackendService
{/*
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    public partial class Startup
    {
        public const string ExternalCookieAuthenticationType = CookieAuthenticationDefaults.AuthenticationType; // ExternalAuthenticationType
        public const string ExternalOAuthAuthenticationType = "ExternalToken";

        static Startup()
        {
            PublicClientId = "self";
			
            //IdentityManagerFactory = new IdentityManagerFactory(IdentityConfig.Settings, () => new IdentityStore());

            CookieOptions = new CookieAuthenticationOptions();

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/Token"),
                AuthorizeEndpointPath = new Microsoft.Owin.PathString("/api/Account/ExternalLogin"),
                //Provider = new ApplicationOAuthProvider(PublicClientId, IdentityManagerFactory, CookieOptions)
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static CookieAuthenticationOptions CookieOptions { get; private set; }

        //public static IdentityManagerFactory IdentityManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }


        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(Owin.IAppBuilder app)
        {
            // Enable the application to use cookies to authenticate users
            //app.UseCookieAuthentication(CookieOptions);

            // Enable the application to use a cookie to store temporary information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(ExternalCookieAuthenticationType);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions, ExternalOAuthAuthenticationType);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");
            
            //app.UseTwitterAuthentication(options => {
            //    options.ConsumerKey = "TcZ4H3Rg92YKwlTKDsestfO8S";
            //    options.ConsumerSecret = "ldALPeUtFfT5W6nFuD9Y6CY4vIRgdeLdzW9k9Z88bZuaUXesrv";
            //});

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }*/
}