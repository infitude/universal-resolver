using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using UniResolver.Services;

namespace UniResolver
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IDidResolverService, NullDidResolverService>();
            services.AddTransient<IDidResolverService, UniResolver.Services.Indy.IndyDidResolverService>();
            // translation resources location
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            // add mvc services with view and annotation localization
            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            // setup supported culture middleware
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-NZ"),
                new CultureInfo("fr-FR")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-NZ"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            // conventional routes for html views, attribute routes for api (see on api controllers)
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default", "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });
            }
            ); 
        }
    }
}
