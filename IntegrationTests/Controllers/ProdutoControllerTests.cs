using Core.DTOs;
using Core.Requests.Create;
using Core.Requests.Delete;
using Core.Requests.Update;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace IntegrationTests.Controllers
{
    public class ProdutoControllerTests : IClassFixture<CustomWebApplicationFactory<ProdutoProdutor.Program>>
    {
        private readonly HttpClient _client;

        public ProdutoControllerTests(CustomWebApplicationFactory<ProdutoProdutor.Program> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/Produto");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var produtos = await response.Content.ReadFromJsonAsync<List<ProdutoDTO>>();
            Assert.NotNull(produtos);
            Assert.True(produtos.Count >= 0);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduto_WhenIdExists()
        {
            // Arrange
            int produtoId = 1;

            // Act
            var response = await _client.GetAsync($"/Produto/{produtoId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var produto = await response.Content.ReadFromJsonAsync<ProdutoDTO>();
            Assert.NotNull(produto);
            Assert.Equal(produtoId, produto.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            int produtoId = 9999;

            // Act
            var response = await _client.GetAsync($"/Produto/{produtoId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAllByCategory_ShouldReturnOk_WhenCategoryExists()
        {
            // Arrange
            short categoryId = 1;

            // Act
            var response = await _client.GetAsync($"/Produto/getAllByCategory/{categoryId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var produtos = await response.Content.ReadFromJsonAsync<List<ProdutoDTO>>();
            Assert.NotNull(produtos);
            Assert.True(produtos.Count >= 0);
        }

        [Fact]
        public async Task GetAllByCategory_ShouldReturnNotResults_WhenCategoryDoesNotExist()
        {
            // Arrange
            short categoryId = 9999;

            // Act
            var response = await _client.GetAsync($"/Produto/getAllByCategory/{categoryId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var produtos = await response.Content.ReadFromJsonAsync<List<ProdutoDTO>>();
            Assert.Equal(0, produtos?.Count);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WhenContatoIsValid()
        {
            // Arrange
            var produtoRequest = new ProdutoRequest()
            {
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/Produto", produtoRequest);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenContatoIsInvalid()
        {
            // Arrange
            var produtoRequest = new ProdutoRequest()
            {
                Nome = "",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync("/Produto", produtoRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnOK_WhenProdutoIsValid()
        {
            // Arrange
            var produtoUpdateRequest = new ProdutoUpdateRequest
            {
                Id = 1,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync("/Produto", produtoUpdateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenUsuarioIsInvalid()
        {
            // Arrange
            var produtoUpdateRequest = new ProdutoUpdateRequest
            {
                Id = 0,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync("/Produto", produtoUpdateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenIdExists()
        {
            // Arrange
            var produtoDeleteRequest = new HttpRequestMessage(HttpMethod.Delete, "/Produto")
            {
                Content = JsonContent.Create(new ProdutoDeleteRequest { Id = 1 })
            };

            // Act
            var response = await _client.SendAsync(produtoDeleteRequest);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
