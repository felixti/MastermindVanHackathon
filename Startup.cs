using MastermindVanHackathon.Configuration;
using MastermindVanHackathon.Controllers;
using MastermindVanHackathon.CrossCutting;
using MastermindVanHackathon.Data;
using MastermindVanHackathon.Models;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.Infrastructure;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Web.Http;

namespace MastermindVanHackathon
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureStaticFiles(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var config = new HttpConfiguration();
            ConfigureRegisters(app, config);
            ConfigureWebApi(app, config);

            //ConfigureOAuth(app);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
                Provider = new AuthAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new PascalCaseToUnderscoreContractResolver();

            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );



            app.UseWebApi(config);
        }

        private void ConfigureStaticFiles(IAppBuilder app)
        {
            SharedOptions sharedOptions = new SharedOptions();
            sharedOptions.FileSystem = new PhysicalFileSystem(new RootPathProvider().GetRootPath());
            StaticFileOptions staticFileOptions = new StaticFileOptions(sharedOptions);

            app.UseStaticFiles(staticFileOptions);
        }

        private void ConfigureRegisters(IAppBuilder app, HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register<IMastermindMatch, MastermindMatch>(Lifestyle.Scoped);
            container.Register<MongoConnection>(Lifestyle.Scoped);
            container.RegisterWebApiRequest<IMastermindRepository, MastermindRepository>();
            container.RegisterWebApiRequest<Game>();
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });
        }
    }
}
