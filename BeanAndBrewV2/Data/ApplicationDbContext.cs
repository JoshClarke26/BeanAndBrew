using BeanAndBrewV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeanAndBrewV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> User {  get; set; }

        public DbSet<BeanAndBrewV2.Models.Coffee> Coffee { get; set; } = default!;

        public DbSet<BeanAndBrewV2.Models.CoffeeOrder> CoffeeOrder { get; set; } = default!;

        public DbSet<BeanAndBrewV2.Models.TableOrder> TableOrder { get; set; } = default!;

        public DbSet<BeanAndBrewV2.Models.BakingLesson> BakingLesson { get; set; } = default!;

        public DbSet<BeanAndBrewV2.Models.BakingLessonSlot> BakingLessonSlot { get; set; } = default!;
    }
}