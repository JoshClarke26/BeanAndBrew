using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanAndBrewV2.Models
{
    public class TableOrder
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Booking Time")]
        public DateTime BookingTime { get; set; }
        [Display(Name = "Number Of People")]
        public int NoOfPeople { get; set; }
        [Display(Name = "Location")]
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
