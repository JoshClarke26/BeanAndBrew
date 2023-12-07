using System.ComponentModel.DataAnnotations;

namespace BeanAndBrewV2.Models
{
    public class BakingLesson
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Lesson Start Time")]
        [Required]
        public DateTime StartTime { get; set; }
        [Display(Name = "Lesson End Time")]
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public string? Product { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public int Price { get; set; }
        public ICollection<BakingLessonSlot>? BakingLessonSlots { get; } = new List<BakingLessonSlot>();
    }
}
