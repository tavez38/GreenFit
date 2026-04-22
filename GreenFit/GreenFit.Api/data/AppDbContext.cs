using GreenFit.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenFet.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Gym> Gym { get; set; }

		public DbSet<Recensione> Recensione { get; set; }
	}
}