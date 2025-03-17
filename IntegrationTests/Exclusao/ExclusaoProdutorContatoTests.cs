using System.Net;

namespace IntegrationTests.Cadastro
{
    public class ExclusaoProdutorContatoTests : IClassFixture<CustomWebApplicationFactory<ExclusaoProdutor.Program>>
    {
        private readonly HttpClient _client;

        public ExclusaoProdutorContatoTests(CustomWebApplicationFactory<ExclusaoProdutor.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenIdExists()
        {
            // Arrange
            int contatoId = 2;

            // Act
            var response = await _client.DeleteAsync($"/Contato/{contatoId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
