using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace YoutubeApi.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            var ver = typeof(Startup).Assembly.GetName().Version.ToString();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API Youtube Test",
                        Version = "v1",
                        Description = "Build: " + ver,
                        Contact = new OpenApiContact
                        {
                            Name = "YoutubeApiTest",
                        }
                    });
            });
        }
    }
}