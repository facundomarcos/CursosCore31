using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest{
            public Guid CursoId {get; set;}
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            // ? -> permite null
            public DateTime? FechaPublicacion {get; set;}
            public List<Guid> ListaInstructor {get; set;}
            public decimal? Precio {get;set;}
            public decimal? Promocion {get;set;}
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
                curso.FechaCreacion = DateTime.UtcNow;

                //logica para actualizar el precio del curso
                var precioEntidad = _context.Precio.Where(x => x.CursoId == curso.CursoId).FirstOrDefault();
                if(precioEntidad!=null){
                    //en caso que sea nulo o vacio
                    precioEntidad.Promocion = request.Promocion ?? precioEntidad.Promocion;
                    precioEntidad.PrecioActual = request.Precio ?? precioEntidad.PrecioActual;
                    //si el precio es null, hay que insertarlo
                }else{
                    precioEntidad = new Precio{
                        PrecioId = Guid.NewGuid(),
                        PrecioActual = request.Precio ?? 0,
                        Promocion = request.Promocion ?? 0,
                        CursoId = curso.CursoId
                    };
                    await _context.Precio.AddAsync(precioEntidad);
                }

                //para actualizar los instructores
                //cuando la lista no sea nula y no este vacia
                if(request.ListaInstructor!=null){
                    if(request.ListaInstructor.Count>0){
                        //eliminar los instructores actuales
                        var instructoresBD = _context.CursoInstructor.Where(x => x.CursoId == request.CursoId).ToList();
                        foreach(var instructorEliminar in instructoresBD){
                            _context.CursoInstructor.Remove(instructorEliminar);
                        }
                        //fin eliminar
                        //adjuntar los instructores que envia el cliente en el request
                        foreach(var ids in request.ListaInstructor){
                            var nuevoInstructor = new CursoInstructor{
                                CursoId = request.CursoId,
                                InstructorId = ids
                            };
                            _context.CursoInstructor.Add(nuevoInstructor);
                        }
                        //fin adjuntar
                    }
                }

                //devuelve la cantidad de filas que se modificaron
                var resultado = await _context.SaveChangesAsync();
                if (resultado>0)
                    return Unit.Value;

                throw new Exception("No se guardaron los cambios en el registro");
            }
        }
    }
}