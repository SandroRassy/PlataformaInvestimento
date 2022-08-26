using Layer.API.Models.DTO;
using Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Layer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendenciaController : ControllerBase
    {
        private readonly ITendenciaService _tendenciaServices;
        public TendenciaController(ITendenciaService tendenciaServices)
        {
            _tendenciaServices = tendenciaServices;
        }

        // GET: api/<TendenciaController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_tendenciaServices.QueryAll());
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        // GET api/<TendenciaController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<TendenciaController>
        [HttpPost]
        public ActionResult Post([FromBody] TendenciaDto value)
        {
            try
            {                
                _tendenciaServices.Inserir(value.Symbol, value.CurrentPrice);                

                return Ok(value);
            }
            catch (Exception exception)
            {                
                return BadRequest($"Erro: {exception.Message}");
            }
        }        
    }
}
