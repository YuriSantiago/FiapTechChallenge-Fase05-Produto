using Core.Requests.Update;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTestsV2.Cadastro
{
    public class AtualizacaoProdutorContatoTests : IClassFixture<CustomWebApplicationFactory<AtualizacaoProdutor.Program>>
    {
        private readonly HttpClient _client;

        public AtualizacaoProdutorContatoTests(CustomWebApplicationFactory<AtualizacaoProdutor.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Update_ShouldReturnOK_WhenContatoIsValid()
        {
            // Arrange
            var contatoUpdateRequest = new ContatoUpdateRequest
            {
                Id = 1,
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            // Act
            var response = await _client.PutAsJsonAsync("/Contato", contatoUpdateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
