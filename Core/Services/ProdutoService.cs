using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Requests.Create;
using Core.Requests.Update;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class ProdutoService : IProdutoService
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IList<ProdutoDTO> GetAll()
        {
            var produtosDTO = new List<ProdutoDTO>();
            var produtos = _produtoRepository.GetAll(c => c.Include(r => r.Categoria));

            foreach (var produto in produtos)
            {
                produtosDTO.Add(new ProdutoDTO()
                {
                    Id = produto.Id,
                    DataInclusao = produto.DataInclusao,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    Disponivel = produto.Disponivel,
                    Categoria = new CategoriaDTO()
                    {
                        Id = produto.Categoria.Id,
                        Descricao = produto.Categoria.Descricao,
                        DataInclusao= produto.DataInclusao
                    }
                });
            }

            return produtosDTO;
        }

        public ProdutoDTO GetById(int id)   
        {
            var produto = _produtoRepository.GetById(id, c => c.Include(r => r.Categoria));

            var produtoDTO = new ProdutoDTO()
            {
                Id = produto.Id,
                DataInclusao = produto.DataInclusao,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Disponivel = produto.Disponivel,
                Categoria = new CategoriaDTO()
                {
                    Id = produto.Categoria.Id,
                    Descricao = produto.Categoria.Descricao,
                    DataInclusao = produto.DataInclusao
                }
            };

            return produtoDTO;
        }

        public IList<ProdutoDTO> GetAllByCategory(short categoria)
        {
            var produtosDTO = new List<ProdutoDTO>();
            var produtos = _produtoRepository.GetAllByCategory(categoria);

            foreach (var produto in produtos)
            {
                produtosDTO.Add(new ProdutoDTO()
                {
                    Id = produto.Id,
                    DataInclusao = produto.DataInclusao,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    Disponivel = produto.Disponivel,
                    Categoria = new CategoriaDTO()
                    {
                        Id = produto.Categoria.Id,
                        Descricao = produto.Categoria.Descricao,
                        DataInclusao = produto.DataInclusao
                    }
                });
            }

            return produtosDTO;
        }

        public void Create(ProdutoRequest produtoRequest)
        {
            var categoria = _categoriaRepository.GetById(produtoRequest.CategoriaId);

            if (categoria is not null)
            {
                var produto = new Produto()
                {
                    Nome = produtoRequest.Nome,
                    Descricao = produtoRequest.Descricao,
                    Preco = produtoRequest.Preco,
                    Disponivel = produtoRequest.Disponivel,
                    CategoriaId = produtoRequest.CategoriaId,
                    Categoria = categoria
                };

                _produtoRepository.Create(produto);
            }
        }

        public void Put(ProdutoUpdateRequest produtoUpdateRequest)
        {
            var produto = _produtoRepository.GetById(produtoUpdateRequest.Id);

            produto.Nome = produtoUpdateRequest.Nome ?? produto.Nome;
            produto.Descricao = produtoUpdateRequest.Descricao ?? produto.Descricao;
            produto.Preco = produtoUpdateRequest.Preco ?? produto.Preco;
            produto.Disponivel = produtoUpdateRequest.Disponivel ?? produto.Disponivel;

            if (produtoUpdateRequest.CategoriaId is not null)
            {
                var categoria = _categoriaRepository.GetById(produtoUpdateRequest.CategoriaId.Value);

                if (categoria is not null)
                    produto.CategoriaId = categoria.Id;
            }

            _produtoRepository.Update(produto);
        }

        public void Delete(int id)
        {
            _produtoRepository.Delete(id);
        }

    }
}
