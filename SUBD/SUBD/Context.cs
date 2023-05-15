using Microsoft.EntityFrameworkCore;
using SUBD.Models;

namespace SUBD
{
	public class Context : DbContext
	{
		public Context()
		{
		}

		public Context(DbContextOptions<Context> options)
		: base(options)
		{
		}

		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductInStore> ProductsInStore { get; set; }
		public virtual DbSet<Store>	Stores { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=192.168.56.101;Port=5432;Database=subdabs;Username=postgres;Password=1");
	}
}