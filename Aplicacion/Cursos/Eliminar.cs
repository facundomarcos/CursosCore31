using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest {
            public Guid Id {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //primero buscamos los instructores del curso y los eliminamos
                var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach(var instructor in instructoresDB){
                    _context.CursoInstructor.Remove(instructor);
                }

                var curso = await _context.Curso.FindAsync(request.Id);
                if(curso==null){
                   // throw new System.Exception("No se pudo eliminar el registro");
                   throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {curso = "No se encontró el curso"});
                }
                _context.Remove(curso);
                var resultado = await _context.SaveChangesAsync();

                if(resultado>0){
                    return Unit.Value;
                }
                throw new System.Exception("No se pudieron guardar los cambios");
                    
            }
        }
    }
}