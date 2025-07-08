using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Services;
using Moq;

namespace UnitTests.Services
{
    public class CategoriaServiceTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly CategoriaService _categoriaService;

        public CategoriaServiceTests()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _categoriaService = new CategoriaService(_categoriaRepositoryMock.Object);
        }

        [Fact]
        public void GetById_ShouldReturnCategoriaDTO_WhenIdExists()
        {
            // Arrange
            var categoria = new Categoria
            {
                Id = 1,
                Descricao = "LANCHE"

            };

            _categoriaRepositoryMock.Setup(repo => repo.GetById(categoria.Id)).Returns(categoria);

            // Act
            var result = _categoriaService.GetById(categoria.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoria.Id, result.Id);
        }
    }
}
