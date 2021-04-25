namespace Jewellery.Api.Core
{
    using Jewellery.Application.Interfaces;
    using Jewellery.Application.Services;
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Interfaces;
    using Jewellery.Persistence;
    using Microsoft.Extensions.DependencyInjection;

    public static class Registry
    {
        public static void ConfigureRegistries(this IServiceCollection services)
        {
            services.AddScoped<IUser, User>();
            services.AddTransient<ILogInService, LogInService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICalculationService, CalculationService>();
            services.AddTransient<IGuidService, GuidService>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ILogInManager, LogInManager>();
        }
    }
}
