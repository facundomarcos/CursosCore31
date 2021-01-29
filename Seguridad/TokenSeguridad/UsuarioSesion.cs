using System.Linq;
using System.Security.Claims;
using Aplicacion.Contratos;
using Microsoft.AspNetCore.Http;

namespace Seguridad
{
    //usamos esta clase para, a traves de la interfaz, obtener datos de la sesion del usuario
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        public UsuarioSesion(IHttpContextAccessor httpContextAccessor){
            _httpContextAccesor = httpContextAccessor;
        }
        public string ObtenerUsuarioSesion()
        {
            var userName = _httpContextAccesor.HttpContext.User?.Claims
            .FirstOrDefault(x => x.Type==ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}