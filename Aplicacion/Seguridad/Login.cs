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
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>{
            public string Email {get;set;}
            public string Password {get;set;}
        }

        //va a validar que email ni password sea vacio con fluentvalidator
        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor(x=>x.Email).NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();

            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager){
                _userManager = userManager;
                _signInManager = signInManager;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //verifica que el email exista
                var usuario = await _userManager.FindByEmailAsync(request.Email);
                if(usuario == null){
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }
                //verificó el mail y despues hay que verificar el password
               var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                //si es exitoso
                if(resultado.Succeeded){
                    return new UsuarioData{
                        NombreCompleto = usuario.NombreCompleto,
                        Token = "datos del token",
                        Username = usuario.UserName,
                        Email = usuario.Email,
                        Imagen = null
                    };
                }

                //no fue exitoso
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);



            }
        }
    }
}