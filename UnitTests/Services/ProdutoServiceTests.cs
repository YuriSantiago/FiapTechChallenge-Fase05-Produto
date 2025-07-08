using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Requests.Create;
using Core.Requests.Update;
using Core.Services;
using Moq;

namespace UnitTests.Services
{
    public class ProdutoServiceTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _categoriaRepositoryMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfProdutoDTO()
        {
            // Arrange
            var produtos = new List<Produto>
            {
               new() {
                    Id = 1,
                    DataInclusao = DateTime.Now,
                    Nome = "Queijo Quente",
                    Descricao = "Pão de forma com uma fatia de queijo",
                    Preco = 10.00M,
                    Disponivel = true,
                    CategoriaId = 1,
                    Categoria = new Categoria { Id = 1, Descricao = "LANCHE" }
               }
             };

            _produtoRepositoryMock.Setup(r => r.GetAll(It.IsAny<Func<IQueryable<Produto>, IQueryable<Produto>>>())).Returns(produtos);

            // Act
            var result = _produtoService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtos.Count, result.Count);
        }

        [Fact]
        public void GetById_ShouldReturnProdutoDTO_WhenIdExists()
        {
            // Arrange
            var produto = new Produto
            {
                Id = 1,
                DataInclusao = DateTime.Now,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,  
                CategoriaId = 1,
                Categoria = new Categoria { Id = 1, Descricao = "LANCHE" }
            };

            _produtoRepositoryMock.Setup(repo => repo.GetById(It.Is<int>(id => id == 1), It.IsAny<Func<IQueryable<Produto>, IQueryable<Produto>>>())).Returns(produto);

            // Act
            var result = _produtoService.GetById(produto.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Id, result.Id);
        }

        [Fact]
        public void GetAllByCategory_ShouldReturnListOfProdutoDTOByCategory()
        {
            // Arrange
            var produtos = new List<Produto>
            {
               new() {
                Id = 1,
                DataInclusao = DateTime.Now,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
                Categoria = new Categoria { Id = 1, Descricao = "LANCHE" }
               }
             };

            _produtoRepositoryMock.Setup(repo => repo.GetAllByCategory(1)).Returns(produtos);

            // Act
            var result = _produtoService.GetAllByCategory(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtos.Count, result.Count);
        }

        [Fact]
        public void Create_ShouldCallRepository_WhenRequestValid()
        {

            // Arrange
            _categoriaRepositoryMock.Setup(r => r.GetById(1)).Returns(new Categoria { Id = 1, Descricao = "LANCHE" });

            var produtoRequest = new ProdutoRequest()
            {
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
            };

            // Act
            _produtoService.Create(produtoRequest);

            // Assert
            _produtoRepositoryMock.Verify(repo => repo.Create(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public void Put_ShouldUpdateUsuario_WhenUsuarioExists()
        {
            // Arrange
            var produtoUpdateRequest = new ProdutoUpdateRequest
            {
                Id = 1,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
            };

            var produto = new Produto
            {
                Id = 1,
                DataInclusao = DateTime.Now,
                Nome = "Queijo Quente",
                Descricao = "Pão de forma com uma fatia de queijo",
                Preco = 10.00M,
                Disponivel = true,
                CategoriaId = 1,
                Categoria = new Categoria { Id = 1, Descricao = "LANCHE" }
            };

            _produtoRepositoryMock.Setup(repo => repo.GetById(produtoUpdateRequest.Id)).Returns(produto);

            // Act
            _produtoService.Put(produtoUpdateRequest);

            // Assert
            _produtoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallRepositoryDelete_WhenIdExists()
        {
            // Arrange
            var id = 1;

            // Act
            _produtoService.Delete(id);

            // Assert
            _produtoRepositoryMock.Verify(repo => repo.Delete(id), Times.Once);
        }
    }
}
