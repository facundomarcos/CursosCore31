using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
         Task<IEnumerable<InstructorModel>> ObtenerLista();
         
        Task<InstructorModel> ObtenerPorId(Guid id);
        //Task<int> porque devuelve la cantidad de transacciones realizadas
        Task<int> Nuevo(InstructorModel parametros);
        Task<int> Actualiza(InstructorModel parametros);
        Task<int> Elimina(Guid id);
    }
}