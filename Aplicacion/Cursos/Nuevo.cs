using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
//al instalar el paquete desde la consola nuget no funciono, se instalo desde la consola
//dotnet add package FluentValidation.AspNetCore
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        
        public class Ejecuta : IRequest {
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime? FechaPublicacion {get; set;}
            //para traer los guid de los instructores
            public List<Guid> ListaInstructor {get;set;}
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
              
            }       
         }

        //esta es la clase que ingresa los cursos en la base de datos
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //valor aleatorio para el guid del curso
               Guid _cursoId = Guid.NewGuid();
                var curso = new Curso {
                    CursoId = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };

                _context.Curso.Add(curso);

                //llamar a la lista de instructores
                if(request.ListaInstructor!=null){
                   
                    foreach(var id in request.ListaInstructor){
                       var cursoInstructor = new CursoInstructor{
                            CursoId = _cursoId,
                            InstructorId = id
                        };
                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }
                //devuelve un entero con la cantidad de operaciones
                var valor = await _context.SaveChangesAsync();
                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el registro");
            }
        }
    }
}