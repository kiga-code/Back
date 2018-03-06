using System;
using System.Collections.Generic;
using kiga.domain.Contracts;
using kiga.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kiga.webapi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IBaseRepository<UsuarioDomain> _UsuarioRepository;

        public UsuarioController(IBaseRepository<UsuarioDomain> UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
        }




        [HttpPost, Route("listarTudo")]
        public JsonResult ListarTudo()
        {
            try
            {
                var all = _UsuarioRepository.Listar();
                return Json(all);
            }
            catch (System.Exception e)
            {
                return Json("Falha ao tentar listar usuários. " + e.Message);
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

        /* [HttpPost]
        public IEnumerable<string> TestePost()
        {

            return new string[] {"batata"};
        } */

        [HttpPost]
        public JsonResult Inserir([FromBody] UsuarioDomain user)
        {
            string p;
            if (user == null)
                return null;

            if (_UsuarioRepository.BuscarFacebookId(user.facebookId) == null)
                try
                {
                    _UsuarioRepository.Inserir(user);

                }
                catch (System.Exception e)
                {
                    p = ("Falha ao tentar autenticar" + e.Message);
                }

            UsuarioDomain User = new UsuarioDomain()
            {
                facebookId = _UsuarioRepository.BuscarFacebookId(user.facebookId).facebookId,
                firstName = _UsuarioRepository.BuscarFacebookId(user.facebookId).firstName,
                lastName = _UsuarioRepository.BuscarFacebookId(user.facebookId).lastName,
                token = _UsuarioRepository.BuscarFacebookId(user.facebookId).id.ToString(),
                id = _UsuarioRepository.BuscarFacebookId(user.facebookId).id
            };
            return Json(User);
        }
    }
}