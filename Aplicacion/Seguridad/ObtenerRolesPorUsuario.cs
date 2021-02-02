using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class ObtenerRolesPorUsuario
    {
        public class Ejecuta : IRequest<List<string>> {
            public string Username {get;set;}
            
        }

        public class Manejador : IRequestHandler<Ejecuta, List<string>>
        {
            private readonly UserManager<Usuario> _userManager;
            public readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager){
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
               {
                //verificar que el usuario existe
                var usuarioIden =await _userManager.FindByNameAsync(request.Username);
                if(usuarioIden == null){
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El usuario no existe"});
                }

                //obtiene todos los roles que tiene un usuario
                var resultados = await _userManager.GetRolesAsync(usuarioIden);
                //devuelve un tipo IList, por eso hay que castearlo
                return new List<string>(resultados) ;

                throw new Exception("No se pudo agregar el Rol al usuario");
            }
            }
        }
    }
}