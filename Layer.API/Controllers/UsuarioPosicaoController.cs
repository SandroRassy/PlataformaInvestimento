using Layer.API.Models.DTO;
using Layer.Domain.Entities;
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
        [HttpGet("{cpf}")]
        public ActionResult Get(string cpf)
        {
            try
            {
                return Ok(_usuarioPosicaoServices.QueryFilter(cpf));   
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        // POST api/<UsuarioPosicaoController>
        [HttpPost]
        public ActionResult Post([FromBody] UsuarioPosicaoDto value)
        {
            try
            {
                UsuarioPosicao posicao = PosicaoFill(value);

                _usuarioPosicaoServices.Inserir(posicao);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        private static UsuarioPosicao PosicaoFill(UsuarioPosicaoDto value)
        {
            var posicao = new UsuarioPosicao();
            posicao.CPF = value.CPF;
            posicao.Positions = new List<Posicao>();

            foreach (var item in value.Positions)
            {
                var objPosicao = new Posicao(item.Symbol, item.Amount, item.CurrentPrice);
                posicao.Positions.Add(objPosicao);
            }

            return posicao;
        }

    }
}
