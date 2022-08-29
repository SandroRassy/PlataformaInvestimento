using Layer.API.Controllers;
using Layer.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Layer.UnitTests.System.Controllers
{
    public class TesteUsuarioController : TestBase
    {
        [Fact]
        public async Task GetAll_Return()
        {
            /// Arrange            
            var sut = new UsuarioController(_usuarioServices);

            /// Act
            var result = (ObjectResult)sut.Get();

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Post_Cpf_ShouldReturn400Status()
        {
            /// Arrange            
            var sut = new UsuarioController(_usuarioServices);

            /// Act
            var obj = new UsuarioDto();

            obj.CPF = "012";
            obj.CodigoConta = "1010";
            obj.Nome = "teste";

            var result = (ObjectResult)sut.Post(obj);

            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
