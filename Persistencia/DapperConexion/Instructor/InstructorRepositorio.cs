using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepositorio : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;
        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }
        public Task<int> Actualiza(InstructorModel parametros)
        {
            throw new NotImplementedException();
        }

        public Task<int> Elimina(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Nuevo(string nombre, string apellidos, string grado)
        {
            var storeProcedure = "usp_instructor_nuevo";

            try
            {
                //conectar
                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                    //nombre del procedimiento
                    storeProcedure,
                    //parametros
                    new
                    {
                        InstructorId = Guid.NewGuid(),
                        Nombre = nombre,
                        Apellidos = apellidos,
                        Grado = grado
                    },
                    //tipo de operacion
                    commandType: CommandType.StoredProcedure

                );
                _factoryConnection.CloseConnection();
                return resultado;


            }
            catch (Exception e)
            {
                throw new Exception("No se pudo guardar el nuevo instructor", e);

            }
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";
            try
            {
                var connection = _factoryConnection.GetConnection();
                //parametros(nombre del storeprocedure, parametros que recibe,especificar el tipo de transaccion  )
                instructorList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);

            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos", e);
                //siempre se ejecuta el finally
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return instructorList;
        }

        public Task<InstructorModel> ObtenerPorId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}