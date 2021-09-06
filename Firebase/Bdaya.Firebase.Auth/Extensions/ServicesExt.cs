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
    public static AuthenticationBuilder AddFirebaseAuthJwt(this AuthenticationBuilder b, FirebaseAuthSettings settings, string schemaName = "Bearer")
    {
        var issuer = "https://securetoken.google.com/" + settings.FirebaseProjectId;
        var metadataAddress = $"{issuer}/.well-known/openid-configuration";
        var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(metadataAddress, new OpenIdConnectConfigurationRetriever());
        // b = b.AddAuthentication(o =>
        //{
        //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        //});
        return b.AddJwtBearer(schemaName, options =>
        {
            options.IncludeErrorDetails = true;
            options.RefreshOnIssuerKeyNotFound = true;
            options.MetadataAddress = metadataAddress;
            options.ConfigurationManager = configurationManager;
            options.Audience = settings.FirebaseProjectId;

        });
    }
}
