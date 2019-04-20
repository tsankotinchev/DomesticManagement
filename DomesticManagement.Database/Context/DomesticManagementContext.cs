using DomesticManagement.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomesticManagement.Database.Context
{
    public class DomesticManagementContext : DbContext
    {
        public DomesticManagementContext(DbContextOptions options)
            : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DomesticResponsibility> DomesticResponsibilities { get; set; }
        public virtual DbSet<DomesticResponsibilityOccurance> DomesticResponsibilityOccurances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DomesticResponsibilityOccurance>()
                .HasKey(bc => bc.Id);
            modelBuilder.Entity<DomesticResponsibilityOccurance>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.DomesticResponsibilityOccurances)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<DomesticResponsibilityOccurance>()
                .HasOne(bc => bc.DomesticResponsibility)
                .WithMany(c => c.DomesticResponsibilityOccurances)
                .HasForeignKey(bc => bc.DomesticResponsibilityId);
        }
    }
}
