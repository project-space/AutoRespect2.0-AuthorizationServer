using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoRespect.AuthorizationServer.Design.Interfaces.DataAccess;
using AutoRespect.AuthorizationServer.DataAccess;
using AutoRespect.AuthorizationServer.Design.Interfaces.Business;
using AutoRespect.AuthorizationServer.Business;

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
            services.AddMvc();

            ConfigureIoC(services);
        }

        public void ConfigureIoC(IServiceCollection services) => services
            .AddSingleton<IUserGetter, UserGetter>()
            .AddSingleton<IUserPasswordAuditor, UserPasswordAuditor>()
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<ITokenIssuer, TokenIssuer>();

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
