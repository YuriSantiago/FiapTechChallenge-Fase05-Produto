using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Requests.Create;
using Core.Requests.Update;
using Core.Services;
using Moq;

namespace FiapTechChallenge.Tests.Services
{
    public class ContatoServiceTests
    {

        private readonly Mock<IContatoRepository> _contatoRepositoryMock;
        private readonly Mock<IRegiaoRepository> _regiaoRepositoryMock;
        private readonly ContatoService _contatoService;

        public ContatoServiceTests()
        {
            _contatoRepositoryMock = new Mock<IContatoRepository>();
            _regiaoRepositoryMock = new Mock<IRegiaoRepository>();
            _contatoService = new ContatoService(_contatoRepositoryMock.Object, _regiaoRepositoryMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfContatoDTO()
        {
            // Arrange
            var contatos = new List<Contato>
            {
               new() {
                   Id = 1,
                   Nome = "Yuri",
                   Telefone = "11999999999",
                   Email = "yuri@email.com",
                   RegiaoId = 1,
                   Regiao = new Regiao { Id = 1, DDD = 11, Descricao = "São Paulo" }
               }
             };

            _contatoRepositoryMock
                .Setup(repo => repo.GetAll(It.IsAny<Func<IQueryable<Contato>, IQueryable<Contato>>>()))
                .Returns(contatos);

            // Act
            var result = _contatoService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Yuri", result.First().Nome);
            Assert.Equal("São Paulo", result.First().Regiao.Descricao);
        }

        [Fact]
        public void GetById_ShouldReturnContatoDTO_WhenIdExists()
        {
            // Arrange
            var contato = new Contato
            {
                Id = 1,
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                RegiaoId = 1,
                Regiao = new Regiao { Id = 1, DDD = 11, Descricao = "São Paulo" }
            };

            _contatoRepositoryMock
                .Setup(repo => repo.GetById(It.Is<int>(id => id == 1), It.IsAny<Func<IQueryable<Contato>, IQueryable<Contato>>>()))
                .Returns(contato);

            // Act
            var result = _contatoService.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Yuri", result.Nome);
            Assert.Equal("São Paulo", result.Regiao.Descricao);
        }

        [Fact]
        public void GetAllByDDD_ShouldReturnListOfContatoDTO_WhenDDDExists()
        {
            // Arrange
            short ddd = 11;

            var contatos = new List<Contato>
            {
               new() {
                   Id = 1,
                   Nome = "Yuri",
                   Telefone = "999999999",
                   Email = "yuri@email.com",
                   RegiaoId = 1,
                   Regiao = new Regiao { Id = 1, DDD = 11, Descricao = "São Paulo" }
               }
             };

            _contatoRepositoryMock.Setup(repo => repo.GetAllByDDD(ddd)).Returns(contatos);

            // Act
            var result = _contatoService.GetAllByDDD(ddd);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contatos.Count, result.Count);
        }

        [Fact]
        public void Create_ShouldAddNewContato_WhenRegiaoExists()
        {
            // Arrange
            var contatoRequest = new ContatoRequest
            {
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                DDD = 11
            };

            var regiao = new Regiao { Id = 1, DDD = 11, Descricao = "São Paulo" };

            _regiaoRepositoryMock
                .Setup(repo => repo.GetByDDD(It.Is<short>(ddd => ddd == 11)))
                .Returns(regiao);

            // Act
            _contatoService.Create(contatoRequest);

            // Assert
            _contatoRepositoryMock.Verify(repo => repo.Create(It.IsAny<Contato>()), Times.Once);
        }

        [Fact]
        public void Put_ShouldUpdateContato_WhenContatoExists()
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

            var contato = new Contato
            {
                Id = 1,
                Nome = "Yuri",
                Telefone = "999999999",
                Email = "yuri@email.com",
                RegiaoId = 1,
                Regiao = new Regiao { Id = 1, DDD = 11, Descricao = "São Paulo" }
            };

            _contatoRepositoryMock.Setup(repo => repo.GetById(contatoUpdateRequest.Id)).Returns(contato);

            // Act
            _contatoService.Put(contatoUpdateRequest);

            // Assert
            _contatoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Contato>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallRepositoryDeleteMethod()
        {
            // Arrange
            var id = 1;

            // Act
            _contatoService.Delete(id);

            // Assert
            _contatoRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
        }



    }
}
