using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
using Persistencia;
using Seguridad;
using WebAPI.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;

namespace WebAPI
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
            services.AddDbContext<CursosOnlineContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddMediatR(typeof(Consulta.Manejador).Assembly);

            services.AddControllers(opt => {
                //para la autorizacion de todos los metodos y no usar decorador [Authorize] 
                //que tengan la autorizacion antes de procesar el request del cliente
                //todos los controller van a tener esa seguridad
                //menos el de login
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
            //agregamos como servicio el coreidentity
            //instancia de la clase usuario
           var builder = services.AddIdentityCore<Usuario>();
           var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
           //instancia de la clase identityframework
           identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();
           //el manejo del login
           identityBuilder.AddSignInManager<SignInManager<Usuario>>();
           
           services.TryAddSingleton<ISystemClock, SystemClock>();

           //para que no se pueda acceder si no se tiene la seguridad del token
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
               opt.TokenValidationParameters = new TokenValidationParameters{
                   ValidateIssuerSigningKey =  true,
                   IssuerSigningKey = key,
                   ValidateAudience = false,
                   ValidateIssuer = false
               };
           });

            //para los tokens de seguridad 
           services.AddScoped<IJwtGenerador, JwtGenerador>();
           //agregamos el servicio para obtener la sesion del usuario
           services.AddScoped<IUsuarioSesion, UsuarioSesion>();
           //agregamos el servicio de automapper para convertir los dto
           services.AddAutoMapper(typeof(Consulta.Manejador));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ManejadorErrorMiddleware>();

            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
