using AtualizacaoProdutor.Controllers;
using Core.Requests.Update;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Atualizacao.Controllers
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
        public async Task Put_ShouldReturnOk_WhenValidContatoRequest()
        {
            // Arrange
            var contatoUpdateRequest = new ContatoUpdateRequest
            {
                Id = 1,
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            _mockBus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(new Mock<ISendEndpoint>().Object);

            // Act
            var result = await _contatoController.Put(contatoUpdateRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Put_ShouldReturnBadRequest_WhenInvalidContatoRequest()
        {
            // Arrange
            var contatoUpdateRequest = new ContatoUpdateRequest
            {
                Id = 0,
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            _mockBus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>())).ThrowsAsync(new Exception("Erro ao atualizar contato"));

            // Act
            var result = await _contatoController.Put(contatoUpdateRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


    }
}
