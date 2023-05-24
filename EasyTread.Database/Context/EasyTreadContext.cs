using EasyTread.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyTread.Database
{
    public class EasyTreadContext : DbContext
    {
        public EasyTreadContext(DbContextOptions<EasyTreadContext> options) : base(options)
        {
        }

        public DbSet<Crossing> Crossing { get; set; }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<LicensePlate> LicensePlate { get; set; }
        public DbSet<PropertySet> PropertySet { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<ShoulderWear> ShoulderWear { get; set; }
        public DbSet<Tire> Tire { get; set; }
        public DbSet<WearPattern> WearPattern { get; set; }
    }
}