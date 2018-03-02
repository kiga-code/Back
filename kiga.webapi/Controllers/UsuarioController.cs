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
/* 
        public UsuarioController (IBaseRepository<UsuarioDomain> UsuarioRepository){
            _UsuarioRepository = UsuarioRepository;
        } */

        [HttpPost]
        public IEnumerable<string> Teste(){

            return new string[] {"batata"};
        }

        /* [HttpGet]
        public IActionResult Teste(){

            return Ok("Batata");
        }
 *//* 
        [HttpPost]
        public IActionResult Create([FromBody] UsuarioDomain user){
            _UsuarioRepository.Inserir(user);
            return Ok();
        } */

    }
}