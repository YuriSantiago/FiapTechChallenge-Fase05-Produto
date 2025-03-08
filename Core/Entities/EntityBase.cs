using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataInclusao { get; set; }

    }
}
