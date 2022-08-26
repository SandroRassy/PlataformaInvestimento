

namespace Layer.UnitTests.System.Services
{
    public class TesteTendenciaService : TestBase
    {
        [Fact]
        public async Task GetAll_Return()
        {
            /// Act
            var result = _tendenciaServices.QueryAll().ToList();

            /// Assert
            result.Should().NotBeEmpty();
        }        
    }
}
