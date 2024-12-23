using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapTechChallenge.Core.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataInclusao { get; set; }

    }
}
