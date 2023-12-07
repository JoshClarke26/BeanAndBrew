using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanAndBrewV2.Models
{
    public class BakingLessonSlot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Amount Of Tickets")]
        public int Tickets { get; set; }
        [ForeignKey("BakingLesson")]
        [Display(Name = "Baking Lesson")]
        public int BakingLessonId {  get; set; }
        public virtual BakingLesson? BakingLesson { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
