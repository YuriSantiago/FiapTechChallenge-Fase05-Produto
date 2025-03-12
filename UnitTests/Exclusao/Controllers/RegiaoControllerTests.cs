using ExclusaoProdutor.Controllers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Exclusao.Controllers
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
        public async Task Delete_ShouldReturnOk_WhenValidId()
        {
            // Arrange
            var id = 1;

            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(new Mock<ISendEndpoint>().Object);

            // Act
            var result = await _regiaoController.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenInvalidId()
        {
            // Arrange
            var id = 0;

            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Erro inesperado"));

            // Act
            var result = await _regiaoController.Delete(id);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}
