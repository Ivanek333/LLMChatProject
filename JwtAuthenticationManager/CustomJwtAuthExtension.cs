using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAuthenticationManager
{
    public static class CustomJwtAuthExtension
    {
        public static void AddCustomJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenHandler.JWT_SECURITY_KEY))
                };
            });
        }

        public static void AddCustomJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserIdMatches", policy =>
                    policy.RequireAssertion(context =>
                    {
                        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "id");
                        if (userIdClaim == null)
                        {
                            return false;
                        }
                        var actionContext = context.Resource as HttpContext;
                        if (actionContext == null)
                        {
                            return false;
                        }
                        if (!actionContext.Request.Query.ContainsKey("userId"))
                        {
                            return true; // method doesn't require userId
                        }
                        var userIdParameter = actionContext.Request.Query["userId"][0]?.ToString();
                        int userIdParam = -1;
                        if (!int.TryParse(userIdParameter, out userIdParam))
                        {
                            return false;
                        }
                        int userId = -1;
                        if (!int.TryParse(userIdClaim.Value, out userId))
                        {
                            return false;
                        }
                        return userId == userIdParam;
                    }));
            });
        }
    }
}
