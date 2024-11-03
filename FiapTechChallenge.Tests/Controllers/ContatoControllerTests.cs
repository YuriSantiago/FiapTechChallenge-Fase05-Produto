using FiapTechChallenge.API.Controllers;
using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FiapTechChallenge.Tests.Controllers
{
    public class ContatoControllerTests
    {

        private readonly Mock<IContatoService> _contatoServiceMock;
        private readonly Mock<IRegiaoService> _regiaoServiceMock;
        private readonly ContatoController _contatoController;

        public ContatoControllerTests()
        {
            _contatoServiceMock = new Mock<IContatoService>();
            _regiaoServiceMock = new Mock<IRegiaoService>();
            _contatoController = new ContatoController(_contatoServiceMock.Object, _regiaoServiceMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkWithListOfContatoDTO()
        {
            // Arrange
            var contatosDTO = new List<ContatoDTO>
            {
               new() {
                   Id = 1,
                   Nome = "Yuri",
                   Telefone = "11999999999",
                   Email = "yuri@email.com",
                   DataInclusao = DateTime.Now,
                   Regiao = new RegiaoDTO { Id = 1, DDD = 11, Descricao = "São Paulo", DataInclusao = DateTime.Now }
               }
             };

            _contatoServiceMock.Setup(service => service.GetAll()).Returns(contatosDTO);

            // Act
            var result = _contatoController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(contatosDTO, okResult.Value);
        }

        [Fact]
        public void GetById_ShouldReturnOkWithContatoDTO_WhenIdExists()
        {
            // Arrange
            var contatoDTO = new ContatoDTO
            {
                Id = 1,
                Nome = "Yuri",
                Telefone = "11999999999",
                Email = "yuri@email.com",
                DataInclusao = DateTime.Now,
                Regiao = new RegiaoDTO { Id = 1, DDD = 11, Descricao = "São Paulo", DataInclusao = DateTime.Now }
            };

            _contatoServiceMock.Setup(service => service.GetById(1)).Returns(contatoDTO);

            // Act
            var result = _contatoController.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(contatoDTO, okResult.Value);
        }

        [Fact]
        public void GetByDDD_ShouldReturnContatos_WhenDDDExists()
        {
            // Arrange
            var contatosDTO = new List<ContatoDTO>
            {
               new() {
                   Id = 1,
                   Nome = "Yuri",
                   Telefone = "11999999999",
                   Email = "yuri@email.com",
                   DataInclusao = DateTime.Now,
                   Regiao = new RegiaoDTO { Id = 1, DDD = 11, Descricao = "São Paulo", DataInclusao = DateTime.Now }
               }
             };

            _contatoServiceMock.Setup(s => s.GetAllByDDD(11)).Returns(contatosDTO);

            // Act
            var result = _contatoController.Get((short)11);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(contatosDTO, okResult.Value);
        }

        [Fact]
        public void Post_ShouldReturnOk_WhenValidContatoRequest()
        {
            // Arrange
            var contatoRequest = new ContatoRequest
            {
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            var regiaoDTO = new RegiaoDTO { Id = 1, DDD = 11, Descricao = "São Paulo", DataInclusao = DateTime.Now };

            _regiaoServiceMock.Setup(service => service.GetByDDD(11)).Returns(regiaoDTO);

            // Act
            var result = _contatoController.Post(contatoRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Put_ShouldReturnOk_WhenValidContatoUpdateRequest()
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

            // Act
            var result = _contatoController.Put(contatoUpdateRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnOk_WhenValidId()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _contatoController.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

    }
}
