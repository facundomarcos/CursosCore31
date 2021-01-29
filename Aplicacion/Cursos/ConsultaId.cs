using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDTO>{
            public Guid Id {get;set;}
        }
        public class Manejador : IRequestHandler<CursoUnico, CursoDTO>
        {
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;
            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CursoDTO> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso
                 //incluir los registros de la tabla instructorcurso 
                .Include(x => x.InstructorLink)
                //e instructor
                .ThenInclude(y =>y.Instructor)
                .FirstOrDefaultAsync(a=> a.CursoId == request.Id);

                if(curso==null){
                   // throw new System.Exception("No se pudo eliminar el registro");
                   throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {curso = "No se encontró el curso"});
                }
                var cursoDTO = _mapper.Map<Curso, CursoDTO>(curso);
                return cursoDTO;
            }
        }
    }
}