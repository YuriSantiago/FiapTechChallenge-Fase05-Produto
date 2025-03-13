using ConsultaFunction;
using Core.DTOs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Consulta
{
    public class ConsultaContatosTests
    {
        private readonly Mock<IContatoService> _contatoServiceMock;
        private readonly Mock<ILogger<ConsultaContatos>> _loggerMock;
        private readonly ConsultaContatos _consultaContatos;

        public ConsultaContatosTests()
        {
            _contatoServiceMock = new Mock<IContatoService>();
            _loggerMock = new Mock<ILogger<ConsultaContatos>>();
            _consultaContatos = new ConsultaContatos(_contatoServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk()
        {
            // Arrange
            var contatos = new List<ContatoDTO>
            {
                new() { Id = 1, Nome = "João", Telefone = "999999999", Email = "joao@email.com", Regiao = new RegiaoDTO { DDD = 11, Descricao = "São Paulo" } },
                new() { Id = 2, Nome = "Maria", Telefone = "988888888", Email = "maria@email.com", Regiao = new RegiaoDTO { DDD = 11, Descricao = "São Paulo" } }
            };

            _contatoServiceMock.Setup(s => s.GetAll()).Returns(contatos);
            var httpContext = new DefaultHttpContext();

            // Act
            var result = _consultaContatos.GetAll(httpContext.Request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(contatos, result.Value);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenValidId()
        {
            // Arrange
            var contato = new ContatoDTO { Id = 1, Nome = "João", Telefone = "11999999999", Email = "joao@email.com", Regiao = new RegiaoDTO { DDD = 11, Descricao = "São Paulo" } };
            _contatoServiceMock.Setup(s => s.GetById(1)).Returns(contato);
            var httpContext = new DefaultHttpContext();

            // Act
            var result = _consultaContatos.GetById(httpContext.Request, 1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(contato, result.Value);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenInvalidId()
        {
            // Arrange
            _contatoServiceMock.Setup(s => s.GetById(It.IsAny<int>())).Throws(new KeyNotFoundException());
            var httpContext = new DefaultHttpContext();

            // Act
            var result = _consultaContatos.GetById(httpContext.Request, 999) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal($"Nenhum contato encontrado com o ID: '999'.", result.Value);
        }

        [Fact]
        public void GetByDDD_ShouldReturnOk()
        {
            // Arrange
            short ddd = 11;

            var contatos = new List<ContatoDTO>
            {
                new() { Id = 1, Nome = "João", Telefone = "11999999999", Email = "joao@email.com", Regiao = new RegiaoDTO { DDD = 11, Descricao = "São Paulo" }}
            };

            _contatoServiceMock.Setup(s => s.GetAllByDDD(ddd)).Returns(contatos);
            var httpContext = new DefaultHttpContext();

            // Act
            var result = _consultaContatos.GetByDDD(httpContext.Request, ddd) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(contatos, result.Value);
        }



    }
}
