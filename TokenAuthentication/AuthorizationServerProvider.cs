using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TokenAuthentication
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override  Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();

            return Task.FromResult<object>(null);
        }
        public override  Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var userName = context.UserName;
                var password = context.Password;
                var userService = new UserService(); // our created one
                var user = userService.ValidateUser(userName, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, Convert.ToString(user.Id)),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                    };
                    ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims,
                                Startup.OAuthOptions.AuthenticationType);

                    var properties = CreateProperties(user.Name);
                    var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                }
                else
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect");
                }
            });
            //UserManager<IdentityUser> userManager = context.OwinContext.GetUserManager<UserManager<IdentityUser>>();
            //IdentityUser user;
            //try
            //{
            //    user = await userManager.FindAsync(context.UserName, context.Password);
            //}
            //catch
            //{
            //    // Could not retrieve the user due to error.  
            //    context.SetError("server_error");
            //    context.Rejected();
            //    return;
            //}
            //if (user != null)
            //{
            //    ClaimsIdentity identity = await userManager.CreateIdentityAsync(
            //                                            user,
            //                                            DefaultAuthenticationTypes.ExternalBearer);
            //    context.Validated(identity);
            //}
            //else
            //{
            //    context.SetError("invalid_grant", "Invalid User Id or password'");
            //    context.Rejected();
            //}
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }


        
        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}