namespace FiapTechChallenge.Core.Entities
{
    public class Regiao : EntityBase
    {

        public required short DDD { get; set; }

        public required string Descricao { get; set; }

        public ICollection<Contato>? Contatos { get; set; }

    }
}
