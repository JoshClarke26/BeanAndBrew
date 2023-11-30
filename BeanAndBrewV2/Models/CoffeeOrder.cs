using System.ComponentModel.DataAnnotations.Schema;

namespace BeanAndBrewV2.Models
{
    public class CoffeeOrder
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        [ForeignKey("Coffee")]
        public int CoffeeId { get; set; }
        public virtual Coffee? Coffee { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
