using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager){
          //si NO existe algun usuario...
            if(!usuarioManager.Users.Any()){
                var usuario = new Usuario{NombreCompleto = "Facundo Marcos", UserName="facundomarcos", Email="facundomarcos@live.com.ar"};
                await usuarioManager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}