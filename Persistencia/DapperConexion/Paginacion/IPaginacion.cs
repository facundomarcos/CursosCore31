using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Paginacion
{
    public interface IPaginacion
    {
        //IDictionary es una coleccion para pasarle los parametros de busqueda
        Task<PaginacionModel> devolverPaginacion(
            string storeProcedure, 
            int numeroPagina, 
            int cantidadElementos, 
            IDictionary<string,object> parametrosFiltro,
            string ordenamientoColumna);        
         

    }
}