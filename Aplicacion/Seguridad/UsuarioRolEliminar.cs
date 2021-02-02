using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioRolEliminar
    {
        public class Ejecuta : IRequest{
             public string Username {get;set;}
            public string RolNombre {get;set;}
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>{
            public EjecutaValidador(){
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _userManager;
            public readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager){
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //verificar que existe el rol
                var role = await _roleManager.FindByNameAsync(request.RolNombre);
                if(role == null){
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El rol no existe"});
                }
                //verificar que el usuario existe
                var usuarioIden =await _userManager.FindByNameAsync(request.Username);
                if(usuarioIden == null){
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El usuario no existe"});
                }

                //remueve el rol del usuario
                var resultado = await _userManager.RemoveFromRoleAsync(usuarioIden, request.RolNombre);
                if(resultado.Succeeded){
                    return Unit.Value;
                }

                throw new Exception("No se pudo remover el Rol al usuario");
            }
        }
    }
}