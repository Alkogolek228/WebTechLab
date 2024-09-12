using Microsoft.EntityFrameworkCore;
using Web_253502_Alkhovik.Domain.Entities;

namespace Web_253502_Alkhovik.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
	    public DbSet<Car> Cars { get; set; }
        
    }
}