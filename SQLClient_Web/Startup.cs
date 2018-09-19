using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SQLClient_Web.Models;
using SQLClient_Web.Repositories;
using SQLClient_Web.Helpers;
using Microsoft.AspNetCore.Http;
using TobitLogger.Core;

namespace SQLClient_Web
{
    public class Startup
    {
        public static string ConnectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILogContextProvider, RequestGuidContextProvider>();

            services.Configure<DataBaseSettings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<DataBaseSettings>(Configuration.GetSection("Security"));

            services.AddSingleton<IDataBaseContext, DataBaseContext>();
            services.AddSingleton<IAuthenticator, Authenticator>();

            services.AddScoped<IRepository<Address>, AddressRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ILogContextProvider logContextProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseRequestLogging();
            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
