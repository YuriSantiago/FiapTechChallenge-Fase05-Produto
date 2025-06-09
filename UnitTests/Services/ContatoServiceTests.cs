//using Core.Entities;
//using Core.Interfaces.Repositories;
//using Core.Requests.Create;
//using Core.Services;
//using Moq;

//namespace UnitTests.Services
//{
//    public class ContatoServiceTests
//    {

//        private readonly Mock<IContatoRepository> _contatoRepositoryMock;
//        private readonly Mock<IRegiaoRepository> _regiaoRepositoryMock;
//        private readonly ContatoService _contatoService;

//        public ContatoServiceTests()
//        {
//            _contatoRepositoryMock = new Mock<IContatoRepository>();
//            _regiaoRepositoryMock = new Mock<IRegiaoRepository>();
//            _contatoService = new ContatoService(_contatoRepositoryMock.Object, _regiaoRepositoryMock.Object);
//        }

//        [Fact]
//        public void Create_ShouldAddNewContato_WhenRegiaoExists()
//        {
//            // Arrange
//            var contatoRequest = new ContatoRequest
//            {
//                Nome = "Yuri",
//                Telefone = "999999999",
//                Email = "yuri@email.com",
//                DDD = 11
//            };

//            var regiao = new Regiao { Id = 1, DDD = 11, Descricao = "São Paulo" };

//            _regiaoRepositoryMock
//                .Setup(repo => repo.GetByDDD(It.Is<short>(ddd => ddd == 11)))
//                .Returns(regiao);

//            // Act
//            _contatoService.Create(contatoRequest);

//            // Assert
//            _contatoRepositoryMock.Verify(repo => repo.Create(It.IsAny<Contato>()), Times.Once);
//        }

//    }
//}
