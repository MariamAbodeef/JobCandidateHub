
using JobCandidateHub.Application.Abstraction;
using JobCandidateHub.Application.Profiles;
using JobCandidateHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JobCandidateHub.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(conf=> conf.AddProfile<MappingProfile>());
            services.AddTransient<IJobCandidateService, JobCandidateService>();

            return services;
        }
    }
}
