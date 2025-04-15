using CadastroProdutor.Controllers;
using Core.DTOs;
using Core.Interfaces.Services;
using Core.Requests.Create;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Cadastro.Controllers
{
    public class ContatoControllerTests
    {

        private readonly Mock<IBus> _mockBus;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IRegiaoService> _mockRegiaoService;
        private readonly ContatoController _contatoController;

        public ContatoControllerTests()
        {
            _mockBus = new Mock<IBus>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockRegiaoService = new Mock<IRegiaoService>();

            _mockConfiguration.Setup(config => config.GetSection("MassTransit:Queues")["ContatoQueue"])
            .Returns("ContatoQueue");

            _contatoController = new ContatoController(_mockBus.Object, _mockConfiguration.Object, _mockRegiaoService.Object);
        }

        [Fact]
        public async Task Post_ShouldReturnOk_WhenValidContatoRequest()
        {
            // Arrange
            var contatoRequest = new ContatoRequest
            {
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            _mockRegiaoService.Setup(service => service.GetByDDD(contatoRequest.DDD)).Returns(new RegiaoDTO() { DDD = 11, Descricao = "São Paulo" });

            _mockBus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(new Mock<ISendEndpoint>().Object);

            // Act
            var result = await _contatoController.Post(contatoRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnNotFound_WhenInvalidDDD()
        {
            // Arrange
            var contatoRequest = new ContatoRequest
            {
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 99
            };

            _mockRegiaoService.Setup(service => service.GetByDDD(contatoRequest.DDD)).Returns(new RegiaoDTO() { DDD = 99, Descricao = "Não Existe" });

            // Act
            var result = await _contatoController.Post(contatoRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


    }
}
