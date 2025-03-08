namespace Core.Requests.Update
{
    public class ContatoUpdateRequest
    {
        public required int Id { get; set; }

        public string? Nome { get; set; }

        public short? DDD { get; set; }

        public string? Telefone { get; set; }

        public string? Email { get; set; }
    }
}
