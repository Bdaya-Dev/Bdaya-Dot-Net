# Spread Auth Manifesto
* This explains detailed handling of cases where auth information is spread between multiple auth registries
* An auth registry is an entity that can issue jwt tokens and store claims about the user
* For this case we will be discussing how to connect firebase auth and asp.net auth

## Problem #1: Source of truth
* Since you have 2 providers, you have multiple sources of truth, so what do you do to solve that?
* There are 2 approaches to this
    1. don't have conflicting info in the first place (easy)
    2. sync info (usually passwords) between providers (hard)


### we are going to discuss how to achieve the (easy) approach
* You can make firebase auth handle all the providers that don't require storing passwords like
    * Facebook
    * Google
    * Phone
    * Twitter
    * Anonymous
    * ...
* While your asp.net auth handles the ones that require passwords like
  * Email with password
  * Email link (firebase won't allow it unless you use Email with password, which makes sense)
  * Username/id with password

## Problem #2: Accounts
* Since you have 2 registries, you have to tell both about your account ... or do you ?
* This is where JWT comes into play.
* Since your backend controllers don't care about the user info, only their claims (what they can and can't do), they only care about whether the jwt is valid or not
* To set this up in asp.net core use the nuget package [Bdaya.Firebase.Auth](https://www.nuget.org/packages/Bdaya.Firebase.Auth/):
  * in your `appSettings.json`
    ```json
    "FirebaseAuthSettings": {
        "FirebaseProjectId": "[projectId]"
    },
    "Jwt": {
        "Issuer": "[whatever-issuer-name]",
        "Audience": "[whatever-audience-name]",
        "Key": "[whatever-random-key]"
    },
    ```
  * in `Startup.cs`
    ```cs
    //using Bdaya.Firebase.Auth;

    //we need the scheme names later too
    const string _firebaseJwt = "Firebase";
    const string _customJwt = "Custom";

    var firebaseJwtParams = configuration.GetSection("FirebaseAuthSettings");
    services.Configure<FirebaseAuthSettings>(firebaseJwtParams);
    var firebaseAuthSettings = firebaseJwtParams.Get<FirebaseAuthSettings>();

    services.AddAuthentication()
        //firebase auth
        .AddFirebaseAuthJwt(schemaName: _firebaseJwt, settings: firebaseAuthSettings) 
        //asp.net auth
        .AddJwtBearer(authenticationScheme: _customJwt, b =>
        {
            b.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });


    services.AddAuthorization(options =>
    {        
        //// Login Auth
        static AuthorizationPolicyBuilder GetCombinedSchemes(AuthorizationPolicyBuilder policy) => policy.AddAuthenticationSchemes(_customJwt, _firebaseJwt);

        options.DefaultPolicy = GetCombinedSchemes(new()).RequireAuthenticatedUser().Build();
        options.AddPolicy("OptionalLogin", policy => GetCombinedSchemes(policy).RequireAssertion(_ => true));
        options.AddPolicy("RequireLogin", policy => GetCombinedSchemes(policy).RequireAuthenticatedUser());
        //Add whatever custom policies and logic you want here, utilizing GetCombinedSchemes
        // it's prefered however that each controller handles its own custom auth rules
    });
    ```

## Problem #3: Syncing claims:
* Now when the user logs in with firebase auth, they get a JWT token with their firebase credentials and you can pass that normally to asp.net controllers
* Only problem is, you still need to tell firebase about what the user can or can't do, so that firebase can put it in its own jwt token
* To do that you simply call `FirebaseAuth.SetCustomUserClaimsAsync` (which takes a json object) whenever you change the user claims
* TIP : pass the claims as a list to match ASP.net format
  ```json
  {
      "aspClaims": [
          {
              "Issuer" : "",
              "Type" : "",
              "Value": "",
              "ValueType" : "",
              "Properties": {
              }
          }
      ]
  }
  ```
  but be careful that claims must not pass 1000 characters when serialized, so you might want to minimize your claim object if you intend to have alot of claims, the minimum is :
  ```json
  {
       "Type" : "", 
       "Value": "",
       "ValueType" : ""
  }
  ```
* you can use that class to handle that
    ```cs
    public class FirebaseClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var newIdentities = new List<ClaimsIdentity>();
            foreach (var identity in principal.Identities)
            {
                var claims = identity.Claims.ToList();
                var fbClaimIndex = claims.FindIndex(x => x.Type == "firebase");
                var fbClaim = fbClaimIndex < 0 ? null : claims[fbClaimIndex];
                if (fbClaim != null && !string.IsNullOrEmpty(fbClaim.Value))
                {
                    var fbRes = JsonSerializer.Deserialize<JsonElement>(fbClaim.Value);

                }

                var aspClaims = principal.FindFirst("aspClaims");
                if (aspClaims != null && !string.IsNullOrEmpty(fbClaim.Value))
                {
                    claims.Remove(aspClaims);
                    var aspRes = JsonSerializer.Deserialize<JsonElement>(aspClaims.Value);
                    foreach (var claimRaw in aspRes.EnumerateArray())
                    {
                        if (!claimRaw.TryGetProperty(nameof(Claim.Type), out var type))
                        {
                            throw new InvalidOperationException($"{nameof(Claim.Type)} Must exist in aspClaims");
                        }
                        if (!claimRaw.TryGetProperty(nameof(Claim.Value), out var value))
                        {
                            throw new InvalidOperationException($"{nameof(Claim.Value)} Must exist in aspClaims");
                        }


                        var valueType = claimRaw.TryGetProperty(nameof(Claim.ValueType), out var valueTypeElement) ?    valueTypeElement.GetString() : ClaimValueTypes.String;
                        var issuer = claimRaw.TryGetProperty(nameof(Claim.Issuer), out var issuerElement) ? issuerElement.GetString () : default;
                        var originalIssuer = claimRaw.TryGetProperty(nameof(Claim.OriginalIssuer), out var originalIssuerElement)   ? originalIssuerElement.GetString() : default;

                        var newClaim = new Claim(type: type.GetString(), value: value.GetString(), valueType: valueType, issuer:    issuer, originalIssuer: originalIssuer);
                        if (claimRaw.TryGetProperty(nameof(Claim.Properties), out var propsElement))
                        {
                            foreach (var propEntry in propsElement.EnumerateObject())
                            {
                                newClaim.Properties.Add(propEntry.Name, propEntry.Value.ToString());
                            }
                        }
                        claims.Add(newClaim);
                    }
                }

                newIdentities.Add(new ClaimsIdentity(identity, claims));
            }

            return Task.FromResult(new ClaimsPrincipal(newIdentities));
        }
    }
    ```
    Then call this somewhere in your ConfigureServices
    ```cs
    services.AddTransient<IClaimsTransformation, FirebaseClaimsTransformer>();
    ```
