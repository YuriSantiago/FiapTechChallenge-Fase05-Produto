//using Core.Requests.Create;
//using System.Net;
//using System.Net.Http.Json;

//namespace IntegrationTests.Cadastro
//{
//    public class CadastroProdutorRegiaoTests : IClassFixture<CustomWebApplicationFactory<CadastroProdutor.Program>>
//    {
//        private readonly HttpClient _client;

//        public CadastroProdutorRegiaoTests(CustomWebApplicationFactory<CadastroProdutor.Program> factory)
//        {
//            _client = factory.CreateClient();
//        }

//        [Fact]
//        public async Task Create_ShouldReturnOk_WhenRegiaoIsValid()
//        {
//            // Arrange
//            var request = new RegiaoRequest { DDD = 11, Descricao = "São Paulo" };

//            // Act
//            var response = await _client.PostAsJsonAsync("/Regiao", request);

//            // Assert
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task Create_ShouldReturnBadRequest_WhenRegiaoIsInvalid()
//        {
//            // Arrange
//            var regiaoRequest = new RegiaoRequest
//            {
//                DDD = 11,
//                Descricao = ""
//            };

//            // Act
//            var response = await _client.PostAsJsonAsync("/Regiao", regiaoRequest);

//            // Assert
//            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//        }

//    }
//}
