using System.ComponentModel.DataAnnotations;

namespace BeanAndBrewV2.Models
{
    public class HamperItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string? Name { get; set; }
        [Display(Name = "Item Description")]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Item Price")]
        [DataType(DataType.Currency)]
        public float? Price { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<HamperItemOrder> ItemOrders { get; } = new List<HamperItemOrder>();

    }
}