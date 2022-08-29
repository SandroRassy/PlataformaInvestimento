using Layer.API.Models.DTO;
using Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Layer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServices;
        public UsuarioController(IUsuarioService usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_usuarioServices.QueryAll());
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public ActionResult Post([FromBody] UsuarioDto value)
        {
            try
            {
                _usuarioServices.Inserir(value.Nome, value.CodigoConta, value.CPF);

                return Ok(value);
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

    }
}
