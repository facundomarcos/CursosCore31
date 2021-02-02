using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Aplicacion.Contratos;
using Dominio;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace Seguridad
{
    //se inyecta como servicio en startup webapi
    public class JwtGenerador : IJwtGenerador
    {
        //recibe parametros usuario y una lista de roles
        public string CrearToken(Usuario usuario, List<string> roles)
        {
            //la informacion relativa al usuario se guarda como claim
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };
            //validar que la lista de roles no es nula
            if(roles != null){
                foreach(var rol in roles){
                    //agrega los roles como claims dentro del token
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }

            //crear credenciales de acceso
            //palabra secreta que va a desencriptar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
            //
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripcion = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales
                
            };

            var tokenManejador = new JwtSecurityTokenHandler();
            //el token se basa en la descripcion declarada arriba para poder ser creado
            var token = tokenManejador.CreateToken(tokenDescripcion);

            return tokenManejador.WriteToken(token);
                
        }
    }
}