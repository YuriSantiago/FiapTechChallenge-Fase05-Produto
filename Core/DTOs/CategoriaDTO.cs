namespace Core.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public required string Descricao { get; set; }
    }
}
