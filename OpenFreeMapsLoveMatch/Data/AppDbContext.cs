using Microsoft.EntityFrameworkCore;
using OpenFreeMapsLoveMatch.Models;

namespace OpenFreeMapsLoveMatch.Data
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Profile> Profiles { get; set; }

    }
}
