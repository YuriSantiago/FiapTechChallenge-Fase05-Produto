namespace FiapTechChallenge.Core.DTOs
{
    public class RegiaoDTO
    {
        public int Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public required short DDD { get; set; }

        public required string Descricao { get; set; }
    }
}
