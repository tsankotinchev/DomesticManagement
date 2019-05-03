using DomesticManagement.Common;
using DomesticManagement.Common.Auditable;
using DomesticManagement.Common.Services.UserResolver;
using DomesticManagement.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomesticManagement.Database.Context
{
    public class DomesticManagementContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IUserResolverService _userService;

        public DomesticManagementContext(DbContextOptions<DomesticManagementContext> options, IUserResolverService userService) : base(options)
        {
            _userService = userService;
        }
        public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.Now;

        public Guid UserProvider
        {
            get
            {
                var userId = _userService.GetUserId() ?? new Guid(Constants.SYSTEM_USER_ID);
                return userId;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void TrackChanges()
        {

            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IAuditable auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedOn = auditable.ModifiedOn = TimestampProvider();
                        auditable.CreatedBy = auditable.ModifiedBy = UserProvider;
                    }
                    else
                    {

                        entry.Property("CreatedOn").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                        auditable.ModifiedOn = TimestampProvider();
                        auditable.ModifiedBy = UserProvider;
                    }
                }

            }
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
