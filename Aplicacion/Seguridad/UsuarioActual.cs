using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    //para devolver a la api, el usuario en sesion
    public class UsuarioActual
    {
        //no tiene parametros, pero va a devolver lo que este en el request, el usuario
        public class Ejecutar : IRequest<UsuarioData>{}

        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion){
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
               var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
               return new UsuarioData{
                   NombreCompleto = usuario.NombreCompleto,
                   Username =  usuario.UserName,
                   Token = _jwtGenerador.CrearToken(usuario),
                   Imagen = null,
                   Email = usuario.Email

               };
                
            }
        }
    }
}