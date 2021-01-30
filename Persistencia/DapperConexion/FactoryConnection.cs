using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguracion> _configs;
        //inyectar la cadena de conexion
        public FactoryConnection(IOptions<ConexionConfiguracion> configs){
            _configs = configs;
        }
        //cuando se abre una conexion, no se puede dejar abierta, entonces usamos este metodo para cerrarla
        public void CloseConnection()
        {
            if(_connection != null && _connection.State == ConnectionState.Open){
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            //si no hay conexion, que la cree
            if(_connection == null){
                _connection = new SqlConnection(_configs.Value.DefaultConnection);
            }
            //si no esta abierta, que la abra
            if(_connection.State != ConnectionState.Open){
                _connection.Open();
            }
            return _connection;
        }
    }
}