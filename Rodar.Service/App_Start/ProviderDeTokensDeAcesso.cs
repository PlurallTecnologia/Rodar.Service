using Microsoft.Owin.Security.OAuth;
using Rodar.Service.Controllers;
using Rodar.Service.Globals;
using Rodar.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Rodar.Service.App_Start
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var facebookLogin = !string.IsNullOrWhiteSpace(context.Request.QueryString.Value) && context.Request.QueryString.Value == "facebookLogin";
            var logged = false;

            if (facebookLogin)
                logged = LoginService.LoginByFacebook(context.UserName);
            else
                logged = LoginService.Login(context.UserName, context.Password);

            if (logged)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

                //LoggedUserInformation.userEmail = context.UserName;

                context.Validated(identity);
            }
            else
            {
                context.SetError("Acesso inválido", "As credenciais do usuário não conferem....");
                return;
            }
        }
    }
}