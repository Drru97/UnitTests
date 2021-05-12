using System;
using System.Net.Mail;
using Data;
using Data.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
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
            services.AddControllers().AddControllersAsServices();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"}); });

            services.AddDbContext<OrderContext>(builder => builder.UseInMemoryDatabase("OrderSystem"));

            services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
            services.AddScoped<IOrderService, PizzaOrderService>();
            services.AddScoped<INotificationService, EmailNotificationService>();
            services.AddScoped<SmtpClient>();
            services.AddSingleton<IDateTimeService, CurrentDateTimeService>();
            services.AddHttpClient<IThirdPartyService, AnalyticsService>(client =>
                client.BaseAddress = new Uri("http://google.com"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
