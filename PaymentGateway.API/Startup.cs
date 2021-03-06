using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PaymentGateway.API.Application.Commands;
using PaymentGateway.API.Filters;
using PaymentGateway.API.Integration.Bank;
using PaymentGateway.Domain;
using PaymentGateway.Infrastructure;
using FluentValidation.AspNetCore;
using FluentValidation;
using PaymentGateway.API.Application.Validations;

namespace PaymentGateway.API
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
            services.AddDbContext<PaymentGatewayContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("PaymentGatewayContext"));
            });

            services.AddScoped<IAquiringBankApiService, AquiringBankApiService>();
            services.AddScoped<IChargeRepository, ChargeRepository>();
            services.AddScoped<IEncryptionService, AESEncryptionService>();

            services.AddTransient<IValidator<CreateChargeCommand>, CreateChargeCommandValidator>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Spheresoft - Payment Gateway HTTP API",
                    Version = "v1",
                    Description = "The Payment Gateway HTTP API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGateway.API v1");
                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
