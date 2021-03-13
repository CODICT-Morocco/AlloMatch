using AlloMatch.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlloMatch.Entities
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        private readonly ICurrentUserService _currentUserService;
        public DbSet<Booking> Booking { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<SoccerField> SoccerField { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<OrganisationMedia> OrganisationMedia { get; set; }

        public DataContext(ICurrentUserService currentUserService,
            DbContextOptions options) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IBaseEntity> entry in ChangeTracker.Entries<IBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
