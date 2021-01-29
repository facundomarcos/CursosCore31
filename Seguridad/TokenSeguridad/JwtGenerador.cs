using Aplicacion.Contratos;
using Dominio;

namespace Seguridad
{
    //se inyecta como servicio en startup webapi
    public class JwtGenerador : IJwtGenerador
    {
        public string CrearToken(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }
    }
}