using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup("eMedOfficeStartup", typeof(eMedOfficeSuite.Startup))]

namespace eMedOfficeSuite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            {
                LoginPath = new Microsoft.Owin.PathString("/user/login"),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }
    }
}
