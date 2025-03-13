using ConsultaFunction;
using Core.DTOs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Consulta
{
    public class ConsultaRegioesTests
    {
        private readonly Mock<IRegiaoService> _regiaoServiceMock;
        private readonly Mock<ILogger<ConsultaRegioes>> _loggerMock;
        private readonly ConsultaRegioes _consultaRegioes;

        public ConsultaRegioesTests()
        {
            _regiaoServiceMock = new Mock<IRegiaoService>();
            _loggerMock = new Mock<ILogger<ConsultaRegioes>>();
            _consultaRegioes = new ConsultaRegioes(_regiaoServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk()
        {
            // Arrange
            var regioes = new List<RegiaoDTO>
            {
                new() { Id = 1, DDD = 11, Descricao = "São Paulo" },
                new() { Id = 2, DDD = 21, Descricao = "Rio de Janeiro" }
            };

            _regiaoServiceMock.Setup(s => s.GetAll()).Returns(regioes);
            var httpContext = new DefaultHttpContext();

            // Act
            var result = _consultaRegioes.GetAll(httpContext.Request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(regioes, result.Value);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenValidId()
        {
            // Arrange
            var regiao = new RegiaoDTO { Id = 1, DDD = 11, Descricao = "São Paulo" };

            _regiaoServiceMock.Setup(s => s.GetById(1)).Returns(regiao);

            var httpContext = new DefaultHttpContext();
            var request = httpContext.Request;

            // Act
            var result = _consultaRegioes.GetById(request, 1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(regiao, result.Value);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenInvalidId()
        {
            // Arrange
            _regiaoServiceMock.Setup(s => s.GetById(It.IsAny<int>())).Throws(new KeyNotFoundException());

            var httpContext = new DefaultHttpContext();
            var request = httpContext.Request;

            // Act
            var result = _consultaRegioes.GetById(request, 999) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void GetByDDD_ShouldReturnOk_WhenValidDDD()
        {
            // Arrange
            var regiao = new RegiaoDTO { Id = 1, DDD = 11, Descricao = "São Paulo" };
            _regiaoServiceMock.Setup(s => s.GetByDDD(11)).Returns(regiao);
            var httpContext = new DefaultHttpContext();

            // Act
            var result = _consultaRegioes.GetByDDD(httpContext.Request, 11) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(regiao, result.Value);
        }

        [Fact]
        public void GetByDDD_ShouldRetunrNotFound_WhenInvalidDDD()
        {
            // Arrange
            _regiaoServiceMock.Setup(s => s.GetByDDD(99)).Returns((RegiaoDTO?)null);

            var httpContext = new DefaultHttpContext();
            var request = httpContext.Request;

            // Act
            var result = _consultaRegioes.GetByDDD(request, 99) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }



    }
}
