namespace Jewellery.Api
{
    using FluentValidation.AspNetCore;
    using Jewellery.Api.Core;
    using Jewellery.Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Reflection;
    using static Jewellery.Domain.Constant;

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
            var assembly = AppDomain.CurrentDomain.Load(ValidationAssembly);
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddFluentValidation(opt =>
                        opt.RegisterValidatorsFromAssembly(Assembly.Load(assembly.GetName())));
            services.ConfigureRegistries();
            var connectionString = Configuration[ConnectionString];
            services.AddDbContext<JewelleryDbContext>(options => options.UseSqlServer(connectionString));
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins(ClientIp).AllowAnyMethod().AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("ApiCorsPolicy");
            app.UseHttpsRedirection();
            app.UseNotAcceptableMiddleware();
            app.UseExceptionMiddleware();
            app.UseMvc();
        }
    }
}
