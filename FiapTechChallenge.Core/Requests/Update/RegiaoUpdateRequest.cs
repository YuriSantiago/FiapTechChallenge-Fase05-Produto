namespace FiapTechChallenge.Core.Requests.Update
{
    public class RegiaoUpdateRequest
    {
        public required int Id { get; set; }

        public short? DDD { get; set; }

        public string? Descricao { get; set; }
    }
}
