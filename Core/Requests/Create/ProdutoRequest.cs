namespace Core.Requests.Create
{
    public class ProdutoRequest
    {
        public required string Nome { get; set; }

        public required string Descricao { get; set; }

        public required decimal Preco { get; set; }

        public required bool Disponivel { get; set; }

        public required int CategoriaId { get; set; }

    }
}
