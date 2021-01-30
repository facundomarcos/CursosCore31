using System.Data;

namespace Persistencia.DapperConexion
{
    public interface IFactoryConnection
    {
        //cerrar conexiones existentes
         void CloseConnection();
         //devuelve objeto de conexion existente
         IDbConnection GetConnection();
    }
}