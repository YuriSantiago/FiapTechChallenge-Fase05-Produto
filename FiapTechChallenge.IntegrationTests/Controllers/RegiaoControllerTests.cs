using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;
using FiapTechChallenge.Infrastructure.Repositories;
using FluentAssertions;
using System.Net;

namespace FiapTechChallenge.IntegrationTests.Controllers
{
    public class RegiaoControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public RegiaoControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenRegiaoIsValid()
        {
            // Arrange
            var regiaoRequest = new RegiaoRequest
            {
                DDD = 11,
                Descricao = "São Paulo"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/Regiao", regiaoRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenRegiaoIsInvalid()
        {
            // Arrange
            var regiaoRequest = new RegiaoRequest
            {
                DDD = 11,
                Descricao = ""
            };

            // Act
            var response = await _client.PostAsJsonAsync("/Regiao", regiaoRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/Regiao");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var regioes = await response.Content.ReadFromJsonAsync<List<RegiaoDTO>>();
            Assert.NotNull(regioes);
            Assert.True(regioes.Count >= 0);
        }

        [Fact]
        public async Task GetById_ShouldReturnRegiao_WhenIdExists()
        {
            // Arrange
            var regiaoId = 1;

            // Act
            var response = await _client.GetAsync($"/Regiao/{regiaoId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var regiao = await response.Content.ReadFromJsonAsync<RegiaoDTO>();
            Assert.NotNull(regiao);
            Assert.Equal(regiaoId, regiao.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnBadRequest_WhenIdDoesNotExist()
        {
            // Arrange
            var nonExistentId = 9999;

            // Act
            var response = await _client.GetAsync($"/Regiao/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
            var response = await _client.PutAsJsonAsync("Regiao/", regiaoUpdateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdDoesNotExist()
        {
            // Arrange
            var regiaoUpdateRequest = new RegiaoUpdateRequest
            {
                Id = 9999,
                DDD = 11,
                Descricao = "São Paulo"
            };

            // Act
            var response = await _client.PutAsJsonAsync("Regiao/", regiaoUpdateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenIdExists()
        {
            // Arrange
            var existingId = 2;

            // Act
            var response = await _client.DeleteAsync($"/Regiao/{existingId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenIdDoesNotExist()
        {
            // Arrange
            var nonExistentId = 9999;

            // Act
            var response = await _client.DeleteAsync($"/Regiao/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

  
}
