﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BlueWhale.Security
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection serviceCollection, string issuer, string audience, string key)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }
    }
}