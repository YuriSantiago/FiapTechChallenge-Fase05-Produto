using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Requests.Create;
using Core.Requests.Delete;
using Core.Requests.Update;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ProdutoProdutor.Controllers;

namespace UnitTests.Controllers
{
    public class ProdutoControllerTests
    {

        private readonly Mock<IBus> _mockBus;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ICategoriaService> _mockCategoriaService;
        private readonly Mock<IProdutoService> _mockProdutoService;
        private readonly ProdutoController _produtoController;

        public ProdutoControllerTests()
        {
            _mockBus = new Mock<IBus>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockCategoriaService = new Mock<ICategoriaService>();
            _mockProdutoService = new Mock<IProdutoService>();
            _produtoController = new ProdutoController(_mockBus.Object, _mockConfiguration.Object, _mockCategoriaService.Object, _mockProdutoService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOkWithProdutos()
        {
            // Arrange
            var produtos = new List<ProdutoDTO>
            {
                new() {
                    Id = 1,
                    DataInclusao = DateTime.Now,
                    Nome = "Queijo Quente",
                    Descricao = "Pão de forma com uma fatia de queijo",
                    Preco = 10.00M,
                    Disponivel = true,
                    Categoria = new CategoriaDTO { Id = 1, DataInclusao = DateTime.Now ,  Descricao = "LANCHE" }
               }
             };

            _mockProdutoService.Setup(s => s.GetAll()).Returns(produtos);

            // Act
            var result = _produtoController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(produtos, okResult.Value);
        }

        [Fact]
        public void GetAll_ShouldReturnBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            _mockProdutoService.Setup(s => s.GetAll()).Throws(new Exception("Erro inesperado"));

            // Act
            var result = _produtoController.Get();

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value?.GetType().GetProperty("mensagem");
            Assert.NotNull(value);
            Assert.Equal("Erro inesperado", value.GetValue(badRequest.Value)?.ToString());
        }

        [Fact]
        public void GetById_ShouldReturnOkWithProduto()
        {
            // Arrange
            var produto = new ProdutoDTO
            {
                Id = 1,
                DataInclusao = DateTime.Now,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                Categoria = new CategoriaDTO { Id = 1, DataInclusao = DateTime.Now, Descricao = "LANCHE" }
            };

            _mockProdutoService.Setup(s => s.GetById(1)).Returns(produto);

            // Act
            var result = _produtoController.Get(1);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ok.StatusCode);
            Assert.Equal(produto, ok.Value);
        }

        [Fact]
        public void GetById_ShouldReturnBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            _mockProdutoService.Setup(s => s.GetById(It.IsAny<int>())).Throws(new Exception("Erro ao buscar ID"));

            // Act
            var result = _produtoController.Get(1);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value?.GetType().GetProperty("mensagem");
            Assert.NotNull(value);
            Assert.Equal("Erro ao buscar ID", value.GetValue(badRequest.Value)?.ToString());
        }

        [Fact]
        public void GetAllByCategory_ShouldReturnOkWithProdutosByCategory()
        {
            // Arrange
            var produtos = new List<ProdutoDTO>
            {
               new() {
                Id = 1,
                DataInclusao = DateTime.Now,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                Categoria = new CategoriaDTO { Id = 1, DataInclusao = DateTime.Now, Descricao = "LANCHE" }
               }
             };

            _mockProdutoService.Setup(s => s.GetAllByCategory(1)).Returns(produtos);

            // Act
            var result = _produtoController.GetAllByCategory(1);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ok.StatusCode);
            Assert.Equal(produtos, ok.Value);
        }

        [Fact]
        public void GetAllByCategory_ShouldReturnBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            _mockProdutoService.Setup(s => s.GetAllByCategory(It.IsAny<int>())).Throws(new Exception("Erro ao buscar por role"));

            // Act
            var result = _produtoController.GetAllByCategory(1);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value?.GetType().GetProperty("mensagem");
            Assert.NotNull(value);
            Assert.Equal("Erro ao buscar por role", value.GetValue(badRequest.Value)?.ToString());
        }

        [Fact]
        public async Task Post_ShouldReturnOk_WhenProdutoRequestIsValid()
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

            var categoria = new CategoriaDTO
            {
                Id = 1,
                Descricao = "LANCHE",
                DataInclusao = DateTime.UtcNow
            };

            var endpointMock = new Mock<ISendEndpoint>();

            _mockCategoriaService.Setup(s => s.GetById(produtoRequest.CategoriaId)).Returns(categoria);
            _mockProdutoService.Setup(s => s.GetAll()).Returns([]);
            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(endpointMock.Object);
            _mockConfiguration.Setup(c => c.GetSection("MassTransit:Queues")["ProdutoCadastroQueue"]).Returns("filaCadastroProduto");

            // Act
            var result = await _produtoController.Post(produtoRequest);

            // Assert
            var ok = Assert.IsType<OkResult>(result);
            endpointMock.Verify(e => e.Send(produtoRequest, default), Times.Once);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenQueueFails()
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

            var categoria = new CategoriaDTO
            {
                Id = 1,
                Descricao = "LANCHE",
                DataInclusao = DateTime.UtcNow
            };

            _mockCategoriaService.Setup(s => s.GetById(produtoRequest.CategoriaId)).Returns(categoria);
            _mockProdutoService.Setup(s => s.GetAll()).Returns([]);
            _mockConfiguration.Setup(c => c.GetSection("MassTransit:Queues")["ProdutoCadastroQueue"]).Returns("filaCadastroProduto");
            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Falha no RabbitMQ"));

            // Act
            var result = await _produtoController.Post(produtoRequest);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value?.GetType().GetProperty("mensagem");
            Assert.NotNull(value);
            Assert.Equal("Falha no RabbitMQ", value.GetValue(badRequest.Value)?.ToString());
        }

        [Fact]
        public async Task Put_ShouldReturnOk_WhenUpdateRequestIsValid()
        {
            // Arrange
            var produtoUpdateRequest = new ProdutoUpdateRequest
            {
                Id = 1,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
            };

            var endpointMock = new Mock<ISendEndpoint>();
            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(endpointMock.Object);
            _mockConfiguration.Setup(c => c.GetSection("MassTransit:Queues")["ProdutoAtualizacaoQueue"]).Returns("filaAtualizacaoProduto");

            // Act
            var result = await _produtoController.Put(produtoUpdateRequest);

            // Assert
            var ok = Assert.IsType<OkResult>(result);
            endpointMock.Verify(e => e.Send(produtoUpdateRequest, default), Times.Once);
        }

        [Fact]
        public async Task Put_ShouldReturnBadRequest_WhenQueueFails()
        {
            // Arrange
            var produtoUpdateRequest = new ProdutoUpdateRequest
            {
                Id = 1,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
            };

            _mockConfiguration.Setup(c => c.GetSection("MassTransit:Queues")["ProdutoAtualizacaoQueue"]).Returns("filaAtualizacaoProduto");

            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Falha na fila"));

            // Act
            var result = await _produtoController.Put(produtoUpdateRequest);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value?.GetType().GetProperty("mensagem");
            Assert.NotNull(value);
            Assert.Equal("Falha na fila", value.GetValue(badRequest.Value)?.ToString());
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenIdIsValid()
        {
            // Arrange
            int id = 1;

            var endpointMock = new Mock<ISendEndpoint>();
            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(endpointMock.Object);
            _mockConfiguration.Setup(c => c.GetSection("MassTransit:Queues")["ProdutoExclusaoQueue"]).Returns("filaExclusaoProduto");

            // Act
            var result = await _produtoController.Delete(new ProdutoDeleteRequest { Id = id });

            // Assert
            var ok = Assert.IsType<OkResult>(result);
            endpointMock.Verify(e => e.Send(It.Is<ProdutoDeleteRequest>(r => r.Id == id), default), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenQueueFails()
        {
            // Arrange
            int id = 1;

            _mockConfiguration.Setup(c => c.GetSection("MassTransit:Queues")["ProdutoExclusaoQueue"]).Returns("filaExclusaoProduto");
            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Falha ao deletar"));

            // Act
            var result = await _produtoController.Delete(new ProdutoDeleteRequest { Id = id });

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value?.GetType().GetProperty("mensagem");
            Assert.NotNull(value);
            Assert.Equal("Falha ao deletar", value.GetValue(badRequest.Value)?.ToString());
        }



    }


}
