using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Renter.Api.Framework;
using Renter.Api.RabbitMq;
using Renter.Core.Domain;
using Renter.Infrastructure.IoC;
using Renter.Infrastructure.Message.Commands;
using Renter.Infrastructure.Mongo;
using Renter.Infrastructure.Services.Interfaces;
using Renter.Infrastructure.Settings;
using Swashbuckle.AspNetCore.Swagger;

namespace Renter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);

            var appSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(appSettingsSection);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44320",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hmp_secret_key_123!")),
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info{ Title = "Core API", Description = "HMP Core API" });
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            builder.AddRabbitMq();
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();
            if (generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }

            var swaggerSettings = app.ApplicationServices.GetService<SwaggerSettings>();
            app.UseSwagger();
            app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API"); });

            MongoConfigurator.Initialize();
            app.UseCustomExceptionHandler();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseRabbitMq()
                .SubscribeCommand<CreateDiscount>();

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
