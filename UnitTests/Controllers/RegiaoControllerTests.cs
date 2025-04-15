using CadastroProdutor.Controllers;
using Core.Requests.Create;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Cadastro.Controllers
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
        public async Task Post_ShouldReturnOk_WhenValidRegiaoRequest()
        {
            // Arrange
            var regiaoRequest = new RegiaoRequest
            {
                DDD = 11,
                Descricao = "São Paulo"
            };

            _mockBus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(new Mock<ISendEndpoint>().Object);

            // Act
            var result = await _regiaoController.Post(regiaoRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenValidRegiaoRequest()
        {
            // Arrange
            var regiaoRequest = new RegiaoRequest
            {
                DDD = 11,
                Descricao = ""
            };

            _mockBus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Erro ao enviar mensagem"));

            // Act
            var result = await _regiaoController.Post(regiaoRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}
