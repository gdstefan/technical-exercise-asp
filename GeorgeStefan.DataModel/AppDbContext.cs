using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgeStefan.DataModel
{
	public class AppDbContext : DbContext
	{
		public DbSet<Country> Countries { get; set; }
		public DbSet<Depot> Depots { get; set; }
		public DbSet<DrugUnit> DrugUnits { get; set; }
		public DbSet<DrugType> DrugTypes { get; set; }
		public DbSet<Site> Sites { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AppDb;Integrated Security=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Country>()
				.HasOne(c => c.Depot)
				.WithMany(d => d.Countries)
				.HasForeignKey(c => c.DepotId);

			modelBuilder.Entity<Depot>()
				.HasMany(d => d.Countries)
				.WithOne(c => c.Depot);

			modelBuilder.Entity<Depot>()
				.HasMany(d => d.DrugUnits)
				.WithOne(du => du.Depot);

			modelBuilder.Entity<DrugUnit>()
				.HasOne(du => du.DrugType)
				.WithMany(dt => dt.DrugUnits)
				.HasForeignKey(du => du.DrugTypeId);

			modelBuilder.Entity<DrugUnit>()
				.HasOne(du => du.Depot)
				.WithMany(d => d.DrugUnits)
				.HasForeignKey(du => du.DepotId);

			modelBuilder.Entity<DrugType>()
				.HasMany(dt => dt.DrugUnits)
				.WithOne(du => du.DrugType);

			modelBuilder.Entity<Site>()
				.HasOne(s => s.Country)
				.WithMany()
				.HasForeignKey(s => s.CountryCode);
		}
	}
}
