namespace Core.Entities
{
    public class Contato : EntityBase
    {

        public required string Nome { get; set; }

        public required string Telefone { get; set; }   

        public required string Email { get; set; }

        public required int RegiaoId { get; set; }

        public required Regiao Regiao { get; set; }

    }
}
