using DomesticManagement.Common.Services.UserResolver;
using DomesticManagement.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DomesticManagement.Database.Context
{
    public class DomesticManagementContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IUserResolverService _userService;

        public DomesticManagementContext(DbContextOptions<DomesticManagementContext> options, IUserResolverService userService) : base(options)
        {
            _userService = userService;
        }
        public virtual DbSet<DomesticResponsibility> DomesticResponsibilities { get; set; }
        public virtual DbSet<DomesticResponsibilityOccurance> DomesticResponsibilityOccurances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);

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
