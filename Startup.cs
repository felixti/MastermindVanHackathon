using Microsoft.Owin.Builder;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.Infrastructure;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MastermindVanHackathon
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SharedOptions sharedOptions = new SharedOptions();
            sharedOptions.FileSystem = new PhysicalFileSystem(new RootPathProvider().GetRootPath());
            StaticFileOptions staticFileOptions = new StaticFileOptions(sharedOptions);


            var config = new HttpConfiguration();

            //config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultHttpRoute",
                routeTemplate: "api/{controller}"
                );

            app.UseStaticFiles(staticFileOptions);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);      
            app.UseWebApi(config);
            ConfigureOAuth(app);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new Microsoft.Owin.PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new AuthAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
