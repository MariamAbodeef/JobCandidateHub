
using JobCandidateHub.Application;
using JobCandidateHub.DataAccessLayer;

namespace JobCandidateHub.API
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddApplicationServices();
            builder.Services.AddDataAccessLayerServices(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Job Candidates Hub Swagger",
                    Version = "V1"
                });

                
            });


            var allowHostsConfiguration = builder.Configuration.GetSection("AllowedHosts");
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("allowCros", policy =>
                {
                    policy.WithOrigins(allowHostsConfiguration.Value)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            
            //builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            //builder.Services.AddTransient<ApiResponseMiddleware>();



            
            return builder.Build();
        }

        
    }
}
