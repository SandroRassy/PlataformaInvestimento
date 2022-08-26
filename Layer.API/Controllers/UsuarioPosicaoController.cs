using Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Layer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPosicaoController : ControllerBase
    {
        private readonly IUsuarioPosicaoService _usuarioPosicaoServices;
        public UsuarioPosicaoController(IUsuarioPosicaoService usuarioPosicaoServices)
        {
            _usuarioPosicaoServices = usuarioPosicaoServices;
        }        

        // GET api/<UsuarioPosicaoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioPosicaoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        
    }
}
