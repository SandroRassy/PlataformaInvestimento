using Layer.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Layer.UnitTests.System.Controllers
{
    public class TesteTendenciaController : TestBase
    {
        [Fact]
        public async Task GetAll_Return()
        {
            /// Arrange            
            var sut = new TendenciaController(_tendenciaServices);

            /// Act
            var result = (ObjectResult)sut.Get();

            /// Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
