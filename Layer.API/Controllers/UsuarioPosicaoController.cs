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
        private readonly ITendenciaService _tendenciaServices;
        public UsuarioPosicaoController(IUsuarioPosicaoService usuarioPosicaoServices, ITendenciaService tendenciaServices)
        {
            _usuarioPosicaoServices = usuarioPosicaoServices;
            _tendenciaServices = tendenciaServices;
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


                var valorProduto = _tendenciaServices.QueryFilter(value.symbol);
                if (valorProduto != null)
                {
                    UsuarioPosicao posicao = PosicaoFill(value, valorProduto.CurrentPrice);

                    _usuarioPosicaoServices.Inserir(posicao);
                    return Ok();
                }
                else
                {
                    return BadRequest($"Produto não encontrado.");
                }

            }
            catch (Exception exception)
            {
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        private static UsuarioPosicao PosicaoFill(UsuarioPosicaoDto value, double valor)
        {
            var posicao = new UsuarioPosicao();

            posicao.CPF = value.cpf;
            posicao.Positions = new List<Posicao>();
            posicao.Positions.Add(new Posicao(value.symbol, value.amount, valor));

            return posicao;
        }

    }
}
