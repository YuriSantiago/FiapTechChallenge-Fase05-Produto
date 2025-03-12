using AtualizacaoProdutor.Controllers;
using Core.Requests.Update;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Atualizacao.Controllers
{
    public class RegiaoControllerTests
    {
        private readonly Mock<IBus> _mockBus;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly RegiaoController _regiaoController;

        public RegiaoControllerTests()
        {
            _mockBus = new Mock<IBus>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(config => config.GetSection("MassTransit:Queues")["RegiaoQueue"])
          .Returns("RegiaoQueue");

            _regiaoController = new RegiaoController(_mockBus.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task Put_ShouldReturnOk_WhenValidRegiaoRequest()
        {
            // Arrange
            var regiaoUpdateRequest = new RegiaoUpdateRequest
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo"
            };

            _mockBus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(new Mock<ISendEndpoint>().Object);

            // Act
            var result = await _regiaoController.Put(regiaoUpdateRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenInvalidRegiaoRequest()
        {
            // Arrange
            var regiaoUpdateRequest = new RegiaoUpdateRequest
            {
                Id = 0,
                DDD = 11,
                Descricao = "São Paulo"
            };

            _mockBus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Erro ao atualizar região"));

            // Act
            var result = await _regiaoController.Put(regiaoUpdateRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}
