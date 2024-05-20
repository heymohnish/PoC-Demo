
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PoC_Demo.DTO;
using PoC_Demo.Repository;
using PoC_Demo.Repository.Interface;
using System.Text;

namespace PoC_Demo.Extension
{
    public static class ServiceExtension 
    {

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository,ProductRepository>();
            services.AddTransient<Connection>();
            return services;
        }

        public static IServiceCollection RegisterAuthendicationSettings(this IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetSection("JWT").Get<JWTSettings>();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 var jwtSettings = services.BuildServiceProvider().GetService<IConfiguration>().GetSection("JWT");
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings["Issuer"],
                     ValidAudience = jwtSettings["Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                 };
             });
            return services;
        }
    }
}
