using System.Linq;
using Aplicacion.Cursos;
using AutoMapper;
using Dominio;

namespace Aplicacion
{
    //para manejar los mapeos de las clases con los dto
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Curso, CursoDTO>()
            //para traen las tablas cursoinstructor e instructor
            .ForMember(x => x.Instructores, y => y.MapFrom(z => z.InstructorLink.Select(a => a.Instructor).ToList()))
             //para traen las tablas comentario
            .ForMember(x => x.Comentarios, y => y.MapFrom(z => z.ComentarioLista))
            //para traen las tablas precio
            .ForMember(x => x.Precio, y => y.MapFrom(z => z.PrecioPromocion));
            CreateMap<CursoInstructor, CursoInstructorDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<Precio, PrecioDTO>();
        }
    }
}