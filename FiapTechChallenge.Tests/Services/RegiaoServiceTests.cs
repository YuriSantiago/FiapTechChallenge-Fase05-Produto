using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Core.Interfaces.Repositories;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;
using FiapTechChallenge.Core.Services;
using Moq;

namespace FiapTechChallenge.Tests.Services
{
    public class RegiaoServiceTests
    {

        private readonly Mock<IRegiaoRepository> _regiaoRepositoryMock;
        private readonly RegiaoService _regiaoService;

        public RegiaoServiceTests()
        {
            _regiaoRepositoryMock = new Mock<IRegiaoRepository>();
            _regiaoService = new RegiaoService(_regiaoRepositoryMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfRegiaoDTO()
        {
            // Arrange
            var regioes = new List<Regiao>
            {
               new() {
                   Id = 1,
                   DDD = 11,
                   Descricao = "São Paulo"
               }
             };

            _regiaoRepositoryMock.Setup(repo => repo.GetAll()).Returns(regioes);

            // Act
            var result = _regiaoService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(regioes.Count, result.Count);
        }

        [Fact]
        public void GetById_ShouldReturnRegiaoDTO_WhenIdExists()
        {
            // Arrange
            var regiao = new Regiao
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo"
            };

            _regiaoRepositoryMock.Setup(repo => repo.GetById(regiao.Id)).Returns(regiao);

            // Act
            var result = _regiaoService.GetById(regiao.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(regiao.Id, result.Id);
        }

        [Fact]
        public void GetByDDD_ShouldReturnRegiaoDTO_WhenDDDExists()
        {
            // Arrange
            short ddd = 11;

            var regiao = new Regiao
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo"
            };

            _regiaoRepositoryMock.Setup(repo => repo.GetByDDD(ddd)).Returns(regiao);

            // Act
            var result = _regiaoService.GetByDDD(ddd);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ddd, result.DDD);
        }

        [Fact]
        public void Create_ShouldCallRepositoryCreate()
        {
            // Arrange
            var regiaoRequest = new RegiaoRequest
            {
                DDD = 11,
                Descricao = "São Paulo"
            };

            // Act
            _regiaoService.Create(regiaoRequest);

            // Assert
            _regiaoRepositoryMock.Verify(repo => repo.Create(It.IsAny<Regiao>()), Times.Once);
        }

        [Fact]
        public void Put_ShouldUpdateRegiao_WhenRegiaoExists()
        {
            // Arrange
            var regiaoUpdateRequest = new RegiaoUpdateRequest
            {
                Id = 1,
                DDD = 11,
                Descricao = "São Paulo"
            };

            var regiao = new Regiao
            {
                Id = 1,
                DDD = 11,
                Descricao = "SP"
            };

            _regiaoRepositoryMock.Setup(repo => repo.GetById(regiaoUpdateRequest.Id)).Returns(regiao);

            // Act
            _regiaoService.Put(regiaoUpdateRequest);

            // Assert
            _regiaoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Regiao>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallRepositoryDelete_WhenIdExists()
        {
            // Arrange
            var id = 1;

            // Act
            _regiaoService.Delete(id);

            // Assert
            _regiaoRepositoryMock.Verify(repo => repo.Delete(id), Times.Once);
        }
    }
}

