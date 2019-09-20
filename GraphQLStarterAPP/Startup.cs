using System;
using System.IO;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using GraphQLStarterAPP.Extensions;
using GraphQLStarterAPP.GraphQL;
using GraphQLStarterAPP.GraphQL.Types;
using GraphQLStarterAPP.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace GraphQLStarterAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // GraphQL Configuration 
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDependencyResolver, ServiceProviderAdapter>();
            services.AddScoped<Query>();
            services.AddScoped<ISchema, RootSchema>();
            // GraphQL Configuration Ends Here 


            // Services and Types Register
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<DevTeamMemberType>();
            // Services and Types Register
            services.ConfigureCors();
            services.AddMemoryCache();

            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseStaticFiles();
            app.UseMvc();
            app.UseGraphiQl();
        }
    }


    internal sealed class ServiceProviderAdapter : IDependencyResolver
    {
        private readonly IServiceProvider _services;

        public ServiceProviderAdapter(IServiceProvider services)
        {
            _services = services.CreateScope().ServiceProvider;
        }

        public T Resolve<T>() => _services.GetRequiredService<T>();

        public object Resolve(Type type)
        {
            return _services.GetRequiredService(type);
        }
    }
}
