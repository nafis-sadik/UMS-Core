using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repositories;
using Services;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
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
            services.AddScoped<IUserInfoRepo, UserInfoRepo>();
            services.AddScoped(typeof(IUserInfoRepo), typeof (UserInfoRepo));

            services.AddScoped<IPassRepo, PassRepo>();
            services.AddScoped(typeof(IPassRepo), typeof(PassRepo));

            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped(typeof(IRoleRepo), typeof(RoleRepo));

            services.AddScoped<IRoleDetailRepo, RoleDetailRepo>();
            services.AddScoped(typeof(IRoleDetailRepo), typeof(RoleDetailRepo));

            services.AddScoped<IAuthqueRepo, AuthqueRepo>();
            services.AddScoped(typeof(IAuthqueRepo), typeof(AuthqueRepo));

            services.AddScoped<IFeatureConfigRepo, FeatureConfigRepo>();
            services.AddScoped(typeof(IFeatureConfigRepo), typeof(FeatureConfigRepo));

            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped(typeof(IUserManagerService), typeof (UserManagerService));

            services.AddScoped<ILogInService, LogInService>();
            services.AddScoped(typeof(ILogInService), typeof (LogInService));

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped(typeof(IRoleService), typeof (RoleService));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            //services.AddCors(o => o.AddPolicy("MyPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
            //services.AddCors(o => o.AddPolicy("MyPolicy", builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader()));
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(45);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
