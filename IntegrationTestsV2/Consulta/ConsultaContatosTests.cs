namespace IntegrationTestsV2
{
    public class ConsultaContatosTests : IClassFixture<CustomWebApplicationFactoryForConsultaContatos>
    {
        private readonly HttpClient _client;

        public ConsultaContatosTests(CustomWebApplicationFactoryForConsultaContatos factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test_GetAllContatos()
        {
            // Act
            var response = await _client.GetAsync("/api/contatos");

            // Assert
            response.EnsureSuccessStatusCode(); // Verifica se o status code é 2xx
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("contatos", responseString); // Verifica se a resposta contém "contatos"
        }

        [Fact]
        public async Task Test_GetContatoPorId()
        {
            // Act
            var response = await _client.GetAsync("/api/contatos/id/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("1", responseString); // Verifica se o ID está na resposta
        }

        [Fact]
        public async Task Test_GetContatosPorDDD()
        {
            // Act
            var response = await _client.GetAsync("/api/contatos/ddd/11");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("11", responseString); // Verifica se o DDD está na resposta
        }
    }
}
