using JobCandidateHub.Application.Abstraction;
using JobCandidateHub.DataAccessLayer.Context;
using JobCandidateHub.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobCandidateHub.DataAccessLayer
{
    public static class DataAccessLayerServicesRegistration
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IJobCandidateRepository, JobCandidateRepository>();


            return services;
        }
    }
}
