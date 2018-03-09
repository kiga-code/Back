using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using kiga.repository.Context;
using kiga.domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Autenticacao_EF_JWT.Controllers
{
    /*
        O método Post receberá requisições HTTP do tipo POST, tendo sido marcado com o atributo AllowAnonymous para assim possibilitar o acesso de usuários não-autenticados;
    As instâncias dos tipos UsersDAO, SigningConfigurations e TokenConfigurations foram marcadas com o atributo FromServices no método Post, o que indica que as mesmas serão resolvidas via mecanismo de injeção de dependências do ASP.NET Core;
    O parâmetro usuario foi marcado com o atributo FromBody, correspondendo às credenciais (ID do usuário + chave de acesso) que serão enviadas no corpo de uma requisição. As informações desta referência (usuario) serão então comparadas com o retorno produzido pela instância do tipo UsersDAO, determinando assim a validade do usuário e da chave de acesso em questão;
    Em se tratando de credenciais de um usuário existente claims serão geradas, o período de expiração calculado e um token criado por meio de uma instância do tipo JwtSecurityTokenHandler (namespace System.IdentityModel.Tokens.Jwt). Este último elemento é então transformado em uma string por meio do método WriteToken e, finalmente, devolvido como retorno da Action Post (juntamente com outras informações como horário de geração e expiração do token);
    Se o usuário for inválido um objeto então será devolvido, indicando que a autenticação falhou.
     */

    [Route("api/login")]
    public class LoginController : Controller
    {
        readonly kigaContexto _contexto;

        public LoginController(kigaContexto contexto)
        {
            _contexto = contexto;
        } 

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]UsuarioDomain usuario,[FromServices]SigningConfigurations signingConfigurations,[FromServices]TokenConfigurations tokenConfigurations)
        {
            /* UsuarioDomain user = _contexto.Usuarios.FirstOrDefault(c => c.facebookId == usuario.facebookId); */
            UsuarioDomain user = _contexto.Usuarios.Include("UsuarioPermissao")
                                                .Include("UsuarioPermissao.Permissao")
                                                .FirstOrDefault(c => c.facebookId == usuario.facebookId);

            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.id.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.id.ToString()),
                        new Claim("Nome", user.firstName),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.facebookId)
                    }
                );

                foreach (var item in user.UsuarioPermissao)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, item.Permissao.nome));
                }

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                var retorno = new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };

                return Ok(retorno);
            }
            else
            {
                var retorno = new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };

                return BadRequest(retorno);
            }
        }
    }
}