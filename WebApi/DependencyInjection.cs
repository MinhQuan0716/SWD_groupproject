using Application.Interface;
using Application.Services;
using WebApi.Services;

namespace WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
