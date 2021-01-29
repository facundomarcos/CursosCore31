using Dominio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostserver = CreateHostBuilder(args).Build();
            using(var ambiente =  hostserver.Services.CreateScope()){
                var services = ambiente.ServiceProvider;

                try{
                    //para insertar un usuario en la base de datos y crear las migraciones
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();
                    var context = services.GetRequiredService<CursosOnlineContext>();
                    context.Database.Migrate();
                    //wait para indicar que hay un metodo asincrono
                    DataPrueba.InsertarData(context, userManager).Wait();
                }catch(Exception e){
                    var logging =  services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(e, "Ocurrió un error en la migración");
                }
            }
            hostserver.Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
