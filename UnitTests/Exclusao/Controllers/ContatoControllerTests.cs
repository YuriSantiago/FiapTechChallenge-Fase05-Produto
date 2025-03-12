using ExclusaoProdutor.Controllers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Exclusao.Controllers
{
    public class ContatoControllerTests
    {

        private readonly Mock<IBus> _mockBus;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly ContatoController _contatoController;

        public ContatoControllerTests()
        {
            _mockBus = new Mock<IBus>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(config => config.GetSection("MassTransit:Queues")["ContatoQueue"])
            .Returns("ContatoQueue");

            _contatoController = new ContatoController(_mockBus.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenValidId()
        {
            // Arrange
            var id = 1;

            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(new Mock<ISendEndpoint>().Object);

            // Act
            var result = await _contatoController.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenInvalidId()
        {
            // Arrange
            var id = 0;

            _mockBus.Setup(b => b.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Erro inesperado"));

            // Act
            var result = await _contatoController.Delete(id);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


    }
}
