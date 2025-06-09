namespace Core.Requests.Update
{
    public class ProdutoUpdateRequest
    {
        public required int Id { get; set; }

        public  string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal? Preco { get; set; }

        public bool? Disponivel { get; set; }

        public int? CategoriaId { get; set; }
    }
}
