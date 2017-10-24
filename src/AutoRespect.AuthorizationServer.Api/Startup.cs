using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.DataAccess;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Business;
using AutoRespect.Infrastructure.OAuth.Jwt;

namespace AutoRespect.AuthorizationServer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            ConfigureIoC(services);
        }

        public void ConfigureIoC(IServiceCollection services) => services
            .AddSingleton<IUserGetter, UserGetter>()
            .AddSingleton<IUserSaver, UserSaver>()
            .AddSingleton<IUserRegistrar, UserRegistrar>()
            .AddSingleton<IUserPasswordAuditor, UserPasswordAuditor>()
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<IJwtIssuer, TokenIssuer>();

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(config =>
            {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
                config.AllowCredentials();
            });
            app.UseMvc();
        }
    }
}
