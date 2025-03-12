using Core.Requests.Create;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTestsV2.Cadastro
{
    public class CadastroProdutorContatoTests : IClassFixture<CustomWebApplicationFactory<CadastroProdutor.Program>>
    {
        private readonly HttpClient _client;

        public CadastroProdutorContatoTests(CustomWebApplicationFactory<CadastroProdutor.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WhenContatoIsValid()
        {
            // Arrange
            var contatoRequest = new ContatoRequest
            {
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            // Act
            var response = await _client.PostAsJsonAsync("/Contato", contatoRequest);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenContatoIsInvalid()
        {
            // Arrange
            var contatoRequest = new ContatoRequest
            {
                Nome = "",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            // Act
            var response = await _client.PostAsJsonAsync("/Contato", contatoRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
