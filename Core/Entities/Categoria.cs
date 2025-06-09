namespace Core.Entities
{
    public class Categoria : EntityBase
    {
        public required string Descricao { get; set; }

        public ICollection<Produto>? Produtos { get; set; }

    }
}
