using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Data;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFactoryConnection _factoryConnection;
        public PaginacionRepositorio(IFactoryConnection factoryConnection){
            _factoryConnection = factoryConnection;
        }
        //devolverPaginacion() es consumido en la capa de negocios
        public async Task<PaginacionModel> devolverPaginacion(string storeProcedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna)
        {
            //es el objeto que va a devolver el task
            PaginacionModel paginacionModel = new PaginacionModel();
            List<IDictionary<string,object>> listaReporte = null;
            //declara e inicializa las variables de salida
            int totalRecords = 0;
            int totalPaginas = 0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                //parametros de entrada que voy a pasar
                DynamicParameters parametros = new DynamicParameters();
                //inserta los filtros dinamicos en el procedimiento de almacenado
                foreach(var param in parametrosFiltro){
                    parametros.Add("@" + param.Key, param.Value);
                }

                parametros.Add("@NumeroPagina", numeroPagina);
                parametros.Add("@CantidadElementos", cantidadElementos);
                parametros.Add("@Ordenamiento", ordenamientoColumna);

                //parametros de salida
                //variable, tipo, salida
                parametros.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parametros.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);


                var result = await connection.QueryAsync(
                    //procedimiento de almacenado
                    storeProcedure,
                    //parametros
                    parametros,
                    //
                    commandType:  System.Data.CommandType.StoredProcedure
                    );
                    //convierte el resutado en un IDictionary
                listaReporte = result.Select(x => (IDictionary<string,object>)x).ToList();
                //listado de todos los registros
                paginacionModel.ListaRecords = listaReporte;
                //cantidad de paginas
                paginacionModel.NumeroPaginas = parametros.Get<int>("@TotalPaginas");
                //cantidad de registros
                paginacionModel.TotalRecords = parametros.Get<int>("@TotalRecords");



            }
            catch (Exception e)
            {
                
                throw new Exception("No se pudo ejecutar el procedimiento de almacenado", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return paginacionModel;
            
        }
    }
}