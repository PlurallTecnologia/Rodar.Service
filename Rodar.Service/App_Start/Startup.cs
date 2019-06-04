using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Rodar.Service.App_Start;
using System.Net.Http.Headers;
using System.Web;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

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

            InicializarFirebase();

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


        private void InicializarFirebase()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(HttpContext.Current.Server.MapPath("~/google-account.json"))
            });
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
