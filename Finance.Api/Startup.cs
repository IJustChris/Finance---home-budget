using Autofac;
using Autofac.Extensions.DependencyInjection;
using Finance.Api.Framework;
using Finance.Infrastructure.Database;
using Finance.Infrastructure.Extensions;
using Finance.Infrastructure.IoC.Modules;
using Finance.Infrastructure.Services;
using Finance.Infrastructure.Settings;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Text;

namespace Finance.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //TEMPORARY CORS FIX
            //TODO: DELETE IN RELEASE VERSION
            services.AddCors(o => o.AddPolicy("MyPolicy", d =>
            {
                d.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            var jwtSettings = Configuration.GetSettings<JwtSettings>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };

            services.AddLogging();
            services.AddMemoryCache();
            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented);
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = tokenValidationParameters;
               });


            services.AddEntityFrameworkNpgsql()
                .AddDbContext<FinanceContext>()
                .BuildServiceProvider();

            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<FinanceContext>()
                .BuildServiceProvider();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            app.UseAuthentication();

            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");


            var generalSettings = Configuration.GetSettings<GeneralSettings>();
            if (generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }

            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
