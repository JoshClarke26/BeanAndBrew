using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BeanAndBrewV2.Models
{
    public class Coffee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [Required]
        public int Size { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<CoffeeOrder>? CoffeeOrders { get; set; }
    }
}
