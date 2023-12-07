using Microsoft.AspNetCore.Identity;

namespace BeanAndBrewV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int StaffPermission { get; set; }
        public ICollection<CoffeeOrder>? CoffeeOrders { get; } = new List<CoffeeOrder>();
        public ICollection<BakingLessonSlot> BakingLessons { get; } = new List<BakingLessonSlot>();
    }
}
