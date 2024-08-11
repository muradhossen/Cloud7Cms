using Common.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class CommonExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("jwtSetting:issuer").Value,
                    ValidAudience = configuration.GetSection("jwtSetting:audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("jwtSetting:secretKey").Value))
                };
            });
        }
        //public static IServiceCollection AddCommonSetting(this IServiceCollection services, IConfiguration configuration,
        //    BaseDbConnections baseDbConnections = null)
        //{
        //    ConfigurationStore.AppName = configuration.GetValue<string>("AppSettings:AppName");

        //    services.AddControllersWithViews().AddNewtonsoftJson(options =>
        //    {
        //        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        //        {
        //            NamingStrategy = new CamelCaseNamingStrategy(),
        //        };
        //    });
        //    return services;
        //}
    }
}
