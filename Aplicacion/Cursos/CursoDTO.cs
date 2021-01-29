using System;
using System.Collections.Generic;

namespace Aplicacion.Cursos
{
    //representa la data que le voy a devolver al cliente
    public class CursoDTO
    {
        public Guid CursoId {get; set;}
        public string Titulo {get; set;}
        public string Descripcion {get; set;}
        //? permite que la fecha sea null
        public DateTime? FechaPublicacion {get; set;}
        public byte[] FotoPortada {get; set;}
        //devuelve el listado de los instructores
        public ICollection<InstructorDTO> Instructores {get;set;}
        
    }


}