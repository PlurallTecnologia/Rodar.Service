using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Rodar.Service.App_Start;
using System.Net.Http.Headers;

[assembly: OwinStartup(typeof(Rodar.Service.Startup))]

namespace Rodar.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            // configuracao WebApi
            var config = new HttpConfiguration();

            // configurando rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{action}/{id}",
                  defaults: new { id = RouteParameter.Optional }
             );

            

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            
            // ativando cors
            app.UseCors(CorsOptions.AllowAll);

            // ativando a geração do token
            AtivarGeracaoTokenAcesso(app);

            // ativando configuração WebApi
            app.UseWebApi(config);

        }

        private void AtivarGeracaoTokenAcesso(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AuthorizeEndpointPath = new PathString("/api/ExternalLogin"),
                Provider = new ProviderDeTokensDeAcesso()
            };

            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }


    //    public static void RegisterComponents()
    //    {
    //        var builder = new ContainerBuilder();
    //        builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
    //        builder.RegisterType<CustomerController>();
    //        builder.RegisterType<Northwind_DBEntities>().As<INorthwind_DBEntities>();
    //        var container = builder.Build();
    //        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    //    }
    }
}
