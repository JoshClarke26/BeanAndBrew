using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanAndBrewV2.Models
{
    public class Hamper
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Username")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public ICollection<HamperItemOrder> HamperItems { get; } = new List<HamperItemOrder>();
        public bool isActive { get; set; }
    }
}
