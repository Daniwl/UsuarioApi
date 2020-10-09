using System.Collections.Generic;
using System.Text.RegularExpressions;
using ApiUsuarios.Models;
using ApiUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers
{
    [Route("api/[Controller]")]
    public class UsuariosController : Controller
    {   
        byte[] key = new byte[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 0, 1, 2 };
        Valida valida = new Valida();
        private readonly IUsuarioRepository _usuarioRepositorio;
        public UsuariosController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepositorio = usuarioRepo;
        }

        #region Http
        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
            return _usuarioRepositorio.GetAll();
        }

        [HttpGet("{id}", Name="GetUsuario")]
        public IActionResult GetById(long id)
        {
            Criptografia criptografia = new Criptografia(key);
            var usuario = _usuarioRepositorio.Find(id);         

            if(usuario == null)
                return NotFound();

            usuario.Senha = criptografia.Descriptografar(usuario.Senha);

            return new ObjectResult(usuario);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Usuario usuario)
        {                  
            Criptografia criptografia = new Criptografia(key);

            if (!valida.Email(usuario.Email) || !valida.Cpf(usuario.Cpf) || usuario == null)
                return BadRequest();
            
            usuario.Nome = usuario.Nome.ToUpper();
            usuario.Senha = criptografia.Criptografar(usuario.Senha);

            _usuarioRepositorio.Add(usuario);
            return CreatedAtRoute("GetUsuario", new { id = usuario.Id}, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]Usuario usuario)
        {
            var criptografia = new Criptografia(key);

            if(usuario == null)
                return NotFound();

            if(usuario.Id != id || !valida.Email(usuario.Email))
                return BadRequest();

            var _usuario = _usuarioRepositorio.Find(id);         
            
            _usuario.Nome = usuario.Nome.ToUpper();
            _usuario.Email = usuario.Email;
            _usuario.Senha = criptografia.Criptografar(usuario.Senha);

            _usuarioRepositorio.Update(_usuario);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = _usuarioRepositorio.Find(id);

            if(usuario == null)
                return NotFound();
            
            _usuarioRepositorio.Remove(id);
            return new NoContentResult();
        }
        #endregion
    }
}