//using Core.Entities;
//using Core.Interfaces.Repositories;
//using Core.Requests.Create;
//using Core.Services;
//using Moq;

//namespace ServiceTests.Services
//{
//    public class RegiaoServiceTests
//    {

//        private readonly Mock<IRegiaoRepository> _regiaoRepositoryMock;
//        private readonly RegiaoService _regiaoService;

//        public RegiaoServiceTests()
//        {
//            _regiaoRepositoryMock = new Mock<IRegiaoRepository>();
//            _regiaoService = new RegiaoService(_regiaoRepositoryMock.Object);
//        }

//        [Fact]
//        public void GetByDDD_ShouldReturnRegiaoDTO_WhenDDDExists()
//        {
//            // Arrange
//            short ddd = 11;

//            var regiao = new Regiao
//            {
//                Id = 1,
//                DDD = 11,
//                Descricao = "São Paulo"
//            };

//            _regiaoRepositoryMock.Setup(repo => repo.GetByDDD(ddd)).Returns(regiao);

//            // Act
//            var result = _regiaoService.GetByDDD(ddd);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(ddd, result.DDD);
//        }

//        [Fact]
//        public void Create_ShouldCallRepositoryCreate()
//        {
//            // Arrange
//            var regiaoRequest = new RegiaoRequest
//            {
//                DDD = 11,
//                Descricao = "São Paulo"
//            };

//            // Act
//            _regiaoService.Create(regiaoRequest);

//            // Assert
//            _regiaoRepositoryMock.Verify(repo => repo.Create(It.IsAny<Regiao>()), Times.Once);
//        }

//    }
//}

