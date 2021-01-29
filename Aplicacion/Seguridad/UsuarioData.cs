namespace Aplicacion.Seguridad
{
    //representa la data que quiero devolver al cliente
     public class UsuarioData
    {
        public string NombreCompleto {get;set;}
        public string Token {get;set;}
        public string Email {get;set;}
        public string Username {get;set;}
        public string Imagen {get;set;}
    }
}