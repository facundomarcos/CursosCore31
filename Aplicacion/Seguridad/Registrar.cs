using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        //http://localhost:5000/api/Usuario/login
        public class Ejecuta : IRequest<UsuarioData>{
            public string Nombre {get;set;}
            public string Apellidos {get;set;}
            public string Email {get;set;}
            public string Password {get;set;}
            public string Username {get;set;}

        }

        //valida que los campos ingresados no sean nulos o vacios
        public class EjecutaValidador : AbstractValidator<Ejecuta>{
            public EjecutaValidador(){
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();


            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador){
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //valida que el email exista en la base de datos
                //retorna un boleano
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if(existe){
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "Ya existe un usuario registrado con este email"});
            }
            //valida el username
            var existeUserName = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
            if(existeUserName){
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "El nombre de usuario ya existe"});
            }
            //crea el usuario
            var usuario = new Usuario {
                NombreCompleto = request.Nombre + " " + request.Apellidos,
                Email = request.Email,
                UserName = request.Username

            };
            //inserta en la base de datos
            //esto da un resultado succes o error
            var resultado = await _userManager.CreateAsync(usuario, request.Password);
            if(resultado.Succeeded){
                return new UsuarioData{
                    NombreCompleto = usuario.NombreCompleto,
                    //la lista de roles es null porque el usuario se acaba de crear
                    Token = _jwtGenerador.CrearToken(usuario, null),
                    Username = usuario.UserName,
                    Email = usuario.Email
                };
            }

            throw new Exception("No se pudo agregar el nuevo usuario");

        }
        }
    }
}