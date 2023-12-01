using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanAndBrewV2.Models
{
    public class TableOrder
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingTime { get; set; }
        public int NoOfPeople { get; set; }
        public LocationList Location { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public enum LocationList
        {
            [Display(Name = "Harrogate")]
            Harrogate,
            [Display(Name = "Leeds")]
            Leeds,
            [Display(Name = "Knaresborough Castle")]
            Knaresborough,
        }
    }
}
