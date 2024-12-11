using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MobilesApi.Models
{
    public class MobileDb: IdentityDbContext<User>
    {
        public MobileDb(DbContextOptions<MobileDb> db): base(db)
        {
            
        }

        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasMany(m => m.Mobiles)
                                        .WithOne(b => b.Brand)
                                        .HasForeignKey(m => m.BrandId);
        }
    }
}