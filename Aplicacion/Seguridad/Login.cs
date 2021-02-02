using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
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
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador){
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;
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
               //agrega la lista de roles que tiene el usuario y que se van a cargar en creartoken
               var resultadoRoles = await _userManager.GetRolesAsync(usuario);
               //parsea a List
               var listaRoles = new List<string>(resultadoRoles);


                //si es exitoso
                if(resultado.Succeeded){
                    return new UsuarioData{
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario, listaRoles),
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