using GPAA.Implementation.Identity;
using GPAA.Models.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using IdentitySample.Models;
using Owin;
using System;

namespace IdentitySample
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and role manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, AspNetUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            ////// Facebook : Create New App
            ////// https://developers.facebook.com/apps
            //const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
            //if (!string.IsNullOrEmpty("1552359324981356"))
            //{
            //    var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions
            //    {
            //        AppId = "1552359324981356",
            //        AppSecret = "ce143d41c0a89b958db415cb2024aed0",
            //        Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider
            //        {
            //            OnAuthenticated = (context) =>
            //            {
            //                context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
            //                foreach (var x in context.User)
            //                {
            //                    var claimType = string.Format("urn:facebook:{0}", x.Key);
            //                    string claimValue = x.Value.ToString();
            //                    if (!context.Identity.HasClaim(claimType, claimValue))
            //                        context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));

            //                }
            //                return Task.FromResult(0);
            //            }
            //        }
            //    };
            //    facebookOptions.Scope.Add("email");
            //    facebookOptions.Scope.Add("public_profile");
            //    facebookOptions.Scope.Add("publish_actions");
            //    facebookOptions.Scope.Add("user_friends");
            //    app.UseFacebookAuthentication(facebookOptions);
            //}

            //app.UseFacebookAuthentication(
            //   appId: "1552359324981356",
            //   appSecret: "ce143d41c0a89b958db415cb2024aed0");


            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            

            //app.UseGoogleAuthentication(
            //    clientId: "886230598195-diekcpe5epjegult3th3np5escccm2kn.apps.googleusercontent.com",
            //    clientSecret: "hSaKj5SvTGCvYRUCF5Qtw088");
        }
    }
}