using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanAndBrewV2.Models
{
    public class HamperItemOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [ForeignKey("HamperItem")]
        [Required]
        [Display(Name = "Hamper Item")]
        public int HamperItemId { get; set; }
        public virtual HamperItem? HamperItem { get; set; }
        [Required]
        [ForeignKey("Hamper")]
        [Display(Name = "Hamper Id")]
        public int HamperId { get; set; }
        public virtual Hamper? Hamper { get; set; }
    }
}
