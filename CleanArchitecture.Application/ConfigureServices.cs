﻿using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.MediatR;
using CleanArchitecture.Application.Common.MediatR.MicrosoftExtensionsDI;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application;

public static class ConfigureServices
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        //  services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}