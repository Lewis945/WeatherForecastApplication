using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherForecast;
using WeatherForecast.Provider;
using WeatherForecast.Provider.DarkSky;
using WeatherForecast.Provider.OpenWeatherMap;
using WeatherForecatRestApi.Logging;
using WeatherForecatRestApi.Models;

namespace WeatherForecatRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<WeatherForecastSecretsModel>(Configuration.GetSection("WeatherForecastSecrets"));

            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton(Configuration);

            services.AddScoped<WeatherForecast.Logging.ILogger, WeatherForecastLogger>(
                p => new WeatherForecastLogger(p.GetService<ILogger<WeatherForecastLogger>>()));

            services.AddScoped(p => new DarkSkyWeatherForecastProvider(
                p.GetService<IOptions<WeatherForecastSecretsModel>>().Value.DarkSky));
            services.AddScoped(p => new OpenWeatherMapProvider(
                p.GetService<IOptions<WeatherForecastSecretsModel>>().Value.OpenWeatherMap));
            services.AddScoped<IWeatherForecastService, WeatherForecastService>(
                p => new WeatherForecastService(new List<IWeatherForecastProvider>
                    {
                        p.GetService<DarkSkyWeatherForecastProvider>(),
                        p.GetService<OpenWeatherMapProvider>()
                    },
                    p.GetService<WeatherForecast.Logging.ILogger>()
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}