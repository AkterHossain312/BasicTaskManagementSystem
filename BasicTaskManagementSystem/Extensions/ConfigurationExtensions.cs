﻿using System.Data;
using System.Text;
using Domain.Interface;
using Domain.Models.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Constants;

namespace BasicTaskManagementSystem.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetSection("Token").Value);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var currentUser = context.HttpContext.RequestServices.GetRequiredService<ICurrentUser>();
                        currentUser.SetClaims(context.Principal.Claims);
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.Configure<ApiBehaviorOptions>(config =>
        {
            config.InvalidModelStateResponseFactory = context =>
            {
                var errMsg = context.ModelState
                    .First(x => x.Value.Errors.Count > 0).Value.Errors.First().ErrorMessage;
                return new BadRequestObjectResult(new { message = errMsg });
            };
        });
        return services;
    }

    public static IServiceCollection AddIdentityOptions(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<TaskManagementDbContext>()
            .AddDefaultTokenProviders()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>();

        services.Configure<IdentityOptions>(option =>
        {
            option.Password.RequireDigit = false;
            option.Password.RequiredLength = 3;
            option.Password.RequireLowercase = false;
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequireUppercase = false;

            // Lockout settings
            option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            option.Lockout.MaxFailedAccessAttempts = 10;

            // User settings

        });
        return services;
    }
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = ConfigurationConstants.SwaggerName, Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization using the Bearer scheme",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, new List<string>()
                }
            });
        });
        return services;
    }
}