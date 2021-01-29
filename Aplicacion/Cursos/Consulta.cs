using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDTO>> {}

        public class Manejador : IRequestHandler<ListaCursos, List<CursoDTO>>
        {
            private readonly CursosOnlineContext _context;
            //para mappear los dto con los entity
            private readonly IMapper _mapper;
            public Manejador(CursosOnlineContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<CursoDTO>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Curso
                //incluir los registros de la tabla instructorcurso 
                .Include(x => x.InstructorLink)
                //e instructor
                .ThenInclude(x =>x.Instructor)
                .ToListAsync();

                //mapeo con los dto
                //los parametros son: el tipo de dato origen, el tipo de dato destino (la data que voy a convertir)
                var cursosDTO = _mapper.Map<List<Curso>, List<CursoDTO>>(cursos);

                return cursosDTO;
            }
        }
    }
}