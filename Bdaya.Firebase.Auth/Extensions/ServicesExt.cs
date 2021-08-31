using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdaya.Firebase.Auth;

public static class ServicesExt
{
    public static AuthenticationBuilder AddFirebaseAuthJwt(this IServiceCollection services, FirebaseAuthSettings settings)
    {
        var issuer = "https://securetoken.google.com/" + settings.FirebaseProjectId;
        var metadataAddress = $"{issuer}/.well-known/openid-configuration";
        var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(metadataAddress, new OpenIdConnectConfigurationRetriever());
        return services.AddAuthentication(o =>
         {
             o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
         }).AddJwtBearer(options =>
         {
             options.IncludeErrorDetails = true;
             options.RefreshOnIssuerKeyNotFound = true;
             options.MetadataAddress = metadataAddress;
             options.ConfigurationManager = configurationManager;
             options.Audience = settings.FirebaseProjectId;
         });
    }
}
