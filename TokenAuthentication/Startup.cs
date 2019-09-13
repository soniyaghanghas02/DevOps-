using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(TokenAuthentication.Startup))]

namespace TokenAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888\
            ConfigureAuth(app);
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }



        //    private void ConfigureOAuth(IAppBuilder app)
        //    {
        //        //app.CreatePerOwinContext<EmployeeDBContext>(() => new EmployeeDBContext());
        //        //app.CreatePerOwinContext<UserManager<IdentityUser>>(CreateManager);

        //        app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
        //        {
        //            TokenEndpointPath = new PathString("/oauth/token"),
        //            Provider = new AuthorizationServerProvider(),
        //            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
        //            AllowInsecureHttp = true,

        //        });
        //        app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        //    }

        //    //private static UserManager<IdentityUser> CreateManager(IdentityFactoryOptions<UserManager<IdentityUser>> options, IOwinContext context)
        //    //{
        //    //    var userStore = new UserStore<IdentityUser>(context.Get<EmployeeDBContext>());
        //    //    var owinManager = new UserManager<IdentityUser>(userStore);
        //    //    return owinManager;
        //    //}
        //}
    }
}
