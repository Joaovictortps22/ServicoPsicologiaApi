using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using servico_atendimento_psicologia.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace servico_atendimento_psicologia.Manager
{
    public class ServiceTokenManager
    {
        public IConfiguration Configuration { get; }

        public JwtDto GerarToken(UsuarioDto usuario)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, usuario.IdUsuario.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MsGuNVY5Fwo3CE3gLhWxu2jDhdki1PKZ"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(5);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: exp,
               signingCredentials: creds);

            return new JwtDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Exp = exp
            };
        }
    }
}
