using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using kiga.domain.Contracts;
using kiga.domain.Entities;
using kiga.repository.Context;
using kiga.repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace kiga.webapi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IBaseRepository<UsuarioDomain> _UsuarioRepository;
        private readonly kigaContexto _contexto;

        public UsuarioController(IBaseRepository<UsuarioDomain> UsuarioRepository, kigaContexto contexto)
        {
            _UsuarioRepository = UsuarioRepository;
            _contexto = contexto;
        }


        [HttpPost]
        public IActionResult Login([FromBody] UsuarioDomain user,
                                [FromServices] SigningConfigurations signingConfigurations,
                                [FromServices] TokenConfigurations tokenConfigurations
                                )
        {
            if (user == null)
                return BadRequest("Usuario inválido.");


            if (_UsuarioRepository.BuscarFacebookId(user.facebookId) == null)
            {
                try
                {
                    _UsuarioRepository.Inserir(user);

                }
                catch (System.Exception e)
                {
                    throw new Exception("Falha ao tentar logar" + e.Message);
                }
            }

            return Ok((new UsuarioDomain()
            {
                facebookId = _UsuarioRepository.BuscarFacebookId(user.facebookId).facebookId,
                firstName = _UsuarioRepository.BuscarFacebookId(user.facebookId).firstName,
                lastName = _UsuarioRepository.BuscarFacebookId(user.facebookId).lastName,
                token = new Token(_contexto).CriarToken(user, signingConfigurations, tokenConfigurations)
            }));
        }



        [HttpGet("{msg}")]
        public JsonResult Get(string msg)
        {
            var rt = _UsuarioRepository.Mensagem(msg);
            JObject o = JObject.Parse(rt);
            return Json(o);
        }

        [HttpPost, Route("listarTudo")]
        public IActionResult ListarTudo([FromBody] UsuarioDomain user)
        {


            if (new Token().ValidaToken(user.token))

                try
                {
                    var all = _UsuarioRepository.Listar();
                    return Ok(all);
                }
                catch (System.Exception e)
                {
                    return BadRequest("Falha ao tentar listar usuários. " + e.Message);
                }

            else
            {
                return BadRequest();
            }
        }




        [HttpPost("{id}"), Route("listarUsuario/{id}")]
        public JsonResult ListarId(int id)
        {
            try
            {
                var user = _UsuarioRepository.BuscarId(id);
                return Json(user);
            }
            catch (System.Exception e)
            {
                return Json("Falha ao tentar Buscar usuário. " + e.Message);
            }
        }




        [HttpDelete("{id}"), Route("apagar/{id}")]

        public IActionResult Apagar(int id)
        {
            try
            {
                var user = _UsuarioRepository.BuscarId(id);
                _UsuarioRepository.Deletar(user);
                return Ok();
            }
            catch (System.Exception e)
            {
                return BadRequest("Falha ao tentar apagar usuário. " + e.Message);
            }
        }



        [HttpPut("{id}"), Route("atualizar/{id}")]
        public IActionResult Atualizar(int id)
        {
            try
            {
                var user = _UsuarioRepository.BuscarId(id);
                _UsuarioRepository.Atualizar(user);
                return Ok();
            }
            catch (System.Exception e)
            {
                return BadRequest("Falha ao tentar atualizar usuário. " + e.Message);
            }
        }






    }
}