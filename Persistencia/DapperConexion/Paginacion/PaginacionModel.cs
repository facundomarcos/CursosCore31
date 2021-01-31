using System.Collections.Generic;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionModel
    {
        // el diccionario devuelve esto, ejemplo
        // [{"cursoId" : "2197383","titulo" : "aspnet"},{"cursoId" : "456456","titulo" : "PHPcurso"}]
        public List<IDictionary<string,object>> ListaRecords {get;set;}
        public int TotalRecords {get;set;}
        public int NumeroPaginas {get;set;}
        

    }
}