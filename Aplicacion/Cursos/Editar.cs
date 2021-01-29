using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest{
            public int CursoId {get; set;}
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            // ? -> permite null
            public DateTime? FechaPublicacion {get; set;}
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
              
            }       
         }

        public class Manejador : IRequestHandler<Ejecuta>
        {
             private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);
               if(curso==null){
                   // throw new System.Exception("No se pudo eliminar el registro");
                   throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {curso = "No se encontró el curso"});
                }
                //si el usuario no modifico el valor, queda el mismo que tenia
                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                //devuelve la cantidad de filas que se modificaron
                var resultado = await _context.SaveChangesAsync();
                if (resultado>0)
                    return Unit.Value;

                throw new Exception("No se guardaron los cambios en el registro");
            }
        }
    }
}