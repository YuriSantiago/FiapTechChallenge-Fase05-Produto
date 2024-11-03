using FiapTechChallenge.API.Controllers;
using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FiapTechChallenge.Tests.Controllers
{
    public class RegiaoControllerTests
    {

        private readonly Mock<IRegiaoService> _regiaoServiceMock;
        private readonly RegiaoController _controller;

        public RegiaoControllerTests()
        {
            _regiaoServiceMock = new Mock<IRegiaoService>();
            _controller = new RegiaoController(_regiaoServiceMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkWithListOfRegiaoDTO()
        {
            // Arrange
            var regioesDTO = new List<RegiaoDTO>
            {
                new() {
                    Id = 1,
                    DDD = 11,
                    Descricao = "São Paulo",
                    DataInclusao = DateTime.Now }
            };

            _regiaoServiceMock.Setup(service => service.GetAll()).Returns(regioesDTO);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(regioesDTO, okResult.Value);
        }

        [Fact]
        public void GetById_ShouldReturnOkWithRegiaoDTO_WhenIdExists()
        {
            // Arrange
            var regiaoDTO = new RegiaoDTO
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo",
                DataInclusao = DateTime.Now
            };

            _regiaoServiceMock.Setup(service => service.GetById(1)).Returns(regiaoDTO);

            // Act
            var result = _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(regiaoDTO, okResult.Value);
        }

        [Fact]
        public void GetByDDD_ShouldReturnOkWithRegiaoDTO_WhenDDDExists()
        {
            // Arrange
            var regiaoDTO = new RegiaoDTO
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo",
                DataInclusao = DateTime.Now
            };

            _regiaoServiceMock.Setup(service => service.GetByDDD(11)).Returns(regiaoDTO);

            // Act
            var result = _controller.Get((short)11);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(regiaoDTO, okResult.Value);
        }

        [Fact]
        public void Post_ShouldReturnOk_WhenValidRegiaoRequest()
        {
            // Arrange
            var regiaoRequest = new RegiaoRequest
            {
                DDD = 11,
                Descricao = "São Paulo"
            };

            // Act
            var result = _controller.Post(regiaoRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Put_ShouldReturnOk_WhenValidRegiaoUpdateRequest()
        {
            // Arrange
            var regiaoUpdateRequest = new RegiaoUpdateRequest
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo"
            };

            // Act
            var result = _controller.Put(regiaoUpdateRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnOk_WhenValidId()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

    }
}
