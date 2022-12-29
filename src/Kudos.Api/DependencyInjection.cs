using FluentValidation;
using Kudos.Data.Context;
using Kudos.Data.Repositories;
using Kudos.Domain.Interfaces;
using Kudos.Services.Dtos;
using Kudos.Services.Services;
using Kudos.Services.Services.MapperProfile;
using Kudos.Services.Validators;
using Microsoft.EntityFrameworkCore;

namespace Kudos.Api
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MapperProfile));

            //var conString = configuration.GetConnectionString("DefaultConnection");  
            //services.AddDbContext<AppDbContext>(options =>
            //                    options.UseNpgsql(conString));

            var connectionString = Environment.GetEnvironmentVariable("DockerCommandsConnectionString");
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

            services.AddTransient<KudoService>();
            services.AddTransient<EmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IKudoRepository, KudoRepository>();

            services.AddTransient<IValidator<EmployeeRequest>, EmployeeValidator>();
            services.AddTransient<IValidator<KudoRequest>, KudoValidator>();
            services.AddTransient<IValidator<DateTime>, DateTimeValidator>();
        }
    }
}
