//using FiapTechChallenge.Core.DTOs;
//using FiapTechChallenge.Core.Requests.Create;
//using FiapTechChallenge.Core.Requests.Update;
//using FluentAssertions;
//using System.Net;

//namespace FiapTechChallenge.IntegrationTests.Controllers
//{
//    public class ContatoControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
//    {
//        private readonly HttpClient _client;

//        public ContatoControllerTests(CustomWebApplicationFactory<Program> factory)
//        {
//            _client = factory.CreateClient();
//        }

//        [Fact]
//        public async Task Create_ShouldReturnCreated_WhenContatoIsValid()
//        {
//            // Arrange
//            var contatoRequest = new ContatoRequest
//            {
//                Nome = "João Silva",
//                Telefone = "987654321",
//                Email = "joao@example.com",
//                DDD = 11
//            };

//            // Act
//            var response = await _client.PostAsJsonAsync("/contato", contatoRequest);

//            // Assert
//            response.StatusCode.Should().Be(HttpStatusCode.OK);
//        }

//        [Fact]
//        public async Task Create_ShouldReturnBadRequest_WhenContatoIsInvalid()
//        {
//            // Arrange
//            var contatoRequest = new ContatoRequest
//            {
//                Nome = "",
//                Telefone = "987654321",
//                Email = "joao@example.com",
//                DDD = 11
//            };

//            // Act
//            var response = await _client.PostAsJsonAsync("/contato", contatoRequest);

//            // Assert
//            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//        }

//        [Fact]
//        public async Task GetAll_ShouldReturnOk()
//        {
//            // Act
//            var response = await _client.GetAsync("/Contato");

//            // Assert
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//            var contatos = await response.Content.ReadFromJsonAsync<List<ContatoDTO>>();
//            Assert.NotNull(contatos);
//            Assert.True(contatos.Count >= 0);
//        }

//        [Fact]
//        public async Task GetById_ShouldReturnContato_WhenIdExists()
//        {
//            // Arrange
//            var contatoId = 1;

//            // Act
//            var response = await _client.GetAsync($"/Contato/{contatoId}");

//            // Assert
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//            var contato = await response.Content.ReadFromJsonAsync<ContatoDTO>();
//            Assert.NotNull(contato);
//            Assert.Equal(contatoId, contato.Id);
//        }

//        [Fact]
//        public async Task GetById_ShouldReturnNotFound_WhenIdDoesNotExist()
//        {
//            // Arrange
//            var nonExistentId = 9999;

//            // Act
//            var response = await _client.GetAsync($"/contato/{nonExistentId}");

//            // Assert
//            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//        }

//        [Fact]
//        public async Task Update_ShouldReturnOK_WhenContatoIsValid()
//        {
//            // Arrange
//            var contatoUpdateRequest = new ContatoUpdateRequest
//            {
//                Id = 1,
//                Nome = "Yuri",
//                Telefone = "999999999",
//                Email = "yuri@email.com",
//                DDD = 11
//            };

//            // Act
//            var response = await _client.PutAsJsonAsync("contato/", contatoUpdateRequest);

//            // Assert
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task Update_ShouldReturnBadRequest_WhenIdDoesNotExist()
//        {
//            // Arrange
//            var contatoUpdateRequest = new ContatoUpdateRequest
//            {
//                Id = 9999,
//                Nome = "Yuri",
//                Telefone = "999999999",
//                Email = "yuri@email.com",
//                DDD = 11
//            };

//            // Act
//           var response = await _client.PutAsJsonAsync("contato/", contatoUpdateRequest);

//            // Assert
//            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//        }

//        [Fact]
//        public async Task Delete_ShouldReturnOk_WhenIdExists()
//        {
//            // Arrange
//            var existingId = 2;

//            // Act
//            var response = await _client.DeleteAsync($"/Contato/{existingId}");

//            // Assert
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task Delete_ShouldReturnNotFound_WhenIdDoesNotExist()
//        {
//            // Arrange
//            var nonExistentId = 9999;

//            // Act
//            var response = await _client.DeleteAsync($"/contato/{nonExistentId}");

//            // Assert
//            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//        }
//    }
//}
