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
            .ForMember(x => x.Instructores, y => y.MapFrom(z => z.InstructorLink.Select(a => a.Instructor).ToList()));
            CreateMap<CursoInstructor, CursoInstructorDTO>();
            CreateMap<Instructor, InstructorDTO>();
        }
    }
}