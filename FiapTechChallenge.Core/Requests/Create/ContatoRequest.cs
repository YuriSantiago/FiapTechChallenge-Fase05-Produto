namespace FiapTechChallenge.Core.Requests.Create
{
    public class ContatoRequest
    {
        public required string Nome { get; set; }

        public required short DDD { get; set; }

        public required string Telefone { get; set; }

        public required string Email { get; set; }
    }
}
