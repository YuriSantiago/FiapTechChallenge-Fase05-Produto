using Core.Requests.Update;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.Cadastro
{
    public class AtualizacaoProdutorRegiaoTests : IClassFixture<CustomWebApplicationFactory<AtualizacaoProdutor.Program>>
    {
        private readonly HttpClient _client;

        public AtualizacaoProdutorRegiaoTests(CustomWebApplicationFactory<AtualizacaoProdutor.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Update_ShouldReturnOK_WhenContatoIsValid()
        {
            // Arrange
            var regiaoUpdateRequest = new RegiaoUpdateRequest
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo"
            };

            // Act
            var response = await _client.PutAsJsonAsync("/Regiao", regiaoUpdateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
