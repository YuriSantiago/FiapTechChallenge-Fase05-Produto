namespace FiapTechChallenge.Core.DTOs
{
    public class ContatoDTO
    {
        public int Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public required string Nome { get; set; }

        public required string Telefone { get; set; }

        public required string Email { get; set; }

        public required RegiaoDTO Regiao { get; set; }



    }
}
