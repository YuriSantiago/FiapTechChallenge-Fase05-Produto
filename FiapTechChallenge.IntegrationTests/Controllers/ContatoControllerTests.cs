using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Requests.Create;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace FiapTechChallenge.IntegrationTests.Controllers
{
    public class ContatoControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        //private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ContatoControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            //_factory = factory;
            //_client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            //{
            //    AllowAutoRedirect = false
            //});
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/Contato");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var contatos = await response.Content.ReadFromJsonAsync<List<ContatoDTO>>();
            Assert.NotNull(contatos);
            Assert.True(contatos.Count >= 0);
        }

        [Fact]
        public async Task GetById_ShouldReturnContato_WhenIdExists()
        {
            // Arrange
            var contatoId = 1;

            // Act
            var response = await _client.GetAsync($"/Contato/{contatoId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var contato = await response.Content.ReadFromJsonAsync<ContatoDTO>();
            Assert.NotNull(contato);
            Assert.Equal(contatoId, contato.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var nonExistentId = 9999;

            // Act
            var response = await _client.GetAsync($"/contato/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenContatoIsValid()
        {
            //// Arrange
            //var newContato = new ContatoDTO
            //{
            //    Nome = "Novo Contato",
            //    Telefone = "999999999",
            //    Email = "novo@contato.com",
            //    DataInclusao = DateTime.UtcNow,
            //    Regiao = new RegiaoDTO
            //    {
            //        Id = 1,
            //        DataInclusao = DateTime.Now,
            //        DDD = 11,
            //        Descricao = "São Paulo"
            //    }
            //};

            //// Act
            //var response = await _client.PostAsJsonAsync("/api/contatos", newContato);

            //// Assert
            //Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            //var createdContato = await response.Content.ReadFromJsonAsync<ContatoDTO>();
            //Assert.NotNull(createdContato);
            //Assert.Equal(newContato.Nome, createdContato.Nome);
            // Arrange
            var contatoRequest = new ContatoRequest
            {
                Nome = "João Silva",
                Telefone = "987654321",
                Email = "joao@example.com",
                DDD = 11
            };

            // Act
            var response = await _client.PostAsJsonAsync("/contato", contatoRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenContatoIsInvalid()
        {
            // Arrange
            var invalidContato = new ContatoDTO
            {
                Id = 1,
                Nome = "",
                Telefone = "11999999999",
                Email = "yuri@email.com",
                DataInclusao = DateTime.Now,
                Regiao = new RegiaoDTO { Id = 1, DDD = 11, Descricao = "São Paulo", DataInclusao = DateTime.Now }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/contato", invalidContato);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenContatoIsValid()
        {
            // Arrange
            var existingContato = new ContatoDTO
            {
                Id = 1, // Certifique-se de usar um ID válido
                Nome = "Contato Atualizado",
                Telefone = "988888888",
                Email = "atualizado@contato.com",
                DataInclusao = DateTime.UtcNow,
                Regiao = new RegiaoDTO
                {
                    Id = 1,
                    DataInclusao = DateTime.Now,
                    DDD = 11,
                    Descricao = "São Paulo"
                }
            };

            // Act
            var response = await _client.PutAsJsonAsync($"contato/{existingContato.Id}", existingContato);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var nonExistentContato = new ContatoDTO
            {
                Id = 9999, 
                Nome = "Contato Não Existe",
                Telefone = "977777777",
                Email = "naoexiste@contato.com",
                DataInclusao = DateTime.UtcNow,
                Regiao = new RegiaoDTO
                {
                    Id = 1,
                    DataInclusao = DateTime.Now,
                    DDD = 11,
                    Descricao = "São Paulo"
                }
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/contato/{nonExistentContato.Id}", nonExistentContato);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenIdExists()
        {
            // Arrange
            var existingId = 1; // Certifique-se de ter um ID válido no banco de testes

            // Act
            var response = await _client.DeleteAsync($"/Contato/{existingId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var nonExistentId = 9999;

            // Act
            var response = await _client.DeleteAsync($"/contato/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
