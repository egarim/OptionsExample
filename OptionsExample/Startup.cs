using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            SimpleConfiguration(services);
            ConfigureAll(services);


            //Consumer
            services.AddScoped<SlackNotificationService>();

        }

        private void ConfigureAll(IServiceCollection services)
        {
            // Configure ALL options instances, both named and default
            //services.ConfigureAll<SlackApiSettings>(Configuration.GetSection("SlackApi:GeneralChannel"));
            services.ConfigureAll<SlackApiSettings>((options) => Configuration.GetSection("SlackApi:GeneralChannel"));
            services.ConfigureAll<SlackApiSettings>(options => options.DisplayName = "Unknown");

            // Override values for named options
            services.Configure<SlackApiSettings>("Dev", Configuration.GetSection("SlackApi:DevChannel"));
            services.Configure<SlackApiSettings>("Public", Configuration.GetSection("SlackApi:PublicChannel"));

            // Override values for default options 
            //this is fixed the original line was this
            //services.Configure<SlackApiSettings>(() => options.DisplayName = "default");

            services.Configure<SlackApiSettings>((options) => options.DisplayName = "default");
        }

        private void SimpleConfiguration(IServiceCollection services)
        {
         

            services.Configure<SlackApiSettings>("Dev", Configuration.GetSection("SlackApi:DevChannel"));
            services.Configure<SlackApiSettings>("General", Configuration.GetSection("SlackApi:GeneralChannel"));
            services.Configure<SlackApiSettings>("Public", Configuration.GetSection("SlackApi:PublicChannel"));

            // Configure the default "unnamed" options
            services.Configure<SlackApiSettings>(Configuration.GetSection("SlackApi:GeneralChannel"));

            // Add required service
            services.AddSingleton<PublicSlackDetailsService>();

            // Add named options configuration AFTER other configuration
            services.AddSingleton<IConfigureOptions<SlackApiSettings>, ConfigurePublicSlackApiSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
