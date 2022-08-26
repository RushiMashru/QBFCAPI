using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using QBFC.Bll;
using QBFC.Bll.Base;
using QBFC.Repos;
using QBFC.Repos.Base;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QBFCAPI
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
            string dbConnectionString = Configuration.GetConnectionString("QBFC");

            services.AddDbContext<QBFCDbcontext>(options =>
            options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QBFC API", Version = "v1", Description = "QBFC API" });

            });

            services.AddTransient<IQbClientBll, QbClientBll>();
            services.AddTransient<IHttpClientBll, HttpClientBll>();
            services.AddTransient<IUtility, Utility>();

            services.AddTransient<IlogsBll, LogsBll>();
            services.AddTransient<IAuthDetailsBll, AuthDetailsBll>();

            services.AddTransient<IQbLogsRepos, QbLogsRepos>();
            services.AddTransient<IQbAuthRepos, QbAuthRepos>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QBFC API");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

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
