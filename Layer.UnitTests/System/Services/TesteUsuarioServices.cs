namespace Layer.UnitTests.System.Services
{
    public class TesteUsuarioServices : TestBase
    {
        [Fact]
        public async Task GetAll_Return()
        {
            /// Act
            var result = _usuarioServices.QueryAll().ToList();

            /// Assert
            result.Should().NotBeEmpty();
        }

        //[Fact]
        //public async Task Insert_ErroCpf_Return()
        //{
        //    /// Act
        //    var result = _usuarioServices.Inserir("Nome", "1010", "123");

        //    /// Assert
        //    result.Should().NotBeEmpty();
        //}
    }
}
