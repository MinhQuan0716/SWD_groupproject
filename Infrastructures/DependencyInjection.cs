using Application;
using Application.Interface;
using Application.InterfaceRepository;
using Application.Services;
using Infrastructures.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures
{
    public static  class DependencyInjection
    { 
        public static IServiceCollection AddInfrastructuresService( this IServiceCollection services,string databaseConnection)
        {
            services.AddScoped<ICurrentTime,CurrentTime>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(databaseConnection).EnableSensitiveDataLogging());
            return services;
        }
    }
}
