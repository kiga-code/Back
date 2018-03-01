using System.Collections.Generic;
using kiga.domain.Contracts;
using kiga.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace kiga.webapi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IBaseRepository<UsuarioDomain> _UsuarioRepository;

        public UsuarioController (IBaseRepository<UsuarioDomain> UsuarioRepository){
            _UsuarioRepository = UsuarioRepository;
        }

        [HttpGet]
        public IEnumerable<UsuarioDomain> GetAll(){
            return _UsuarioRepository.Listar();
        }

        [HttpPost]
        public IActionResult Create([FromBody] UsuarioDomain user){
            _UsuarioRepository.Inserir(user);
            return Ok();
        }
    }
}