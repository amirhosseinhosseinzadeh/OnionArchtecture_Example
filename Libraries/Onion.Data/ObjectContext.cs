using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Onion.Data.Mappings;
using Onion.Domain.Bases;
using Onion.Domain.Security;
using Onion.Domain.Users;
using Onion.Libraries.Domain.Security;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Onnion.Data
{
    public  class ObjectContext : DbContext
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public ObjectContext(DbContextOptions options,
                            IHttpContextAccessor httpContextAccessor
                            )
            : base(options)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Utilities


        protected virtual void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    var now = DateTime.UtcNow;
                    var modifierId = GetLoggedInEmployee();

                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.IsUpdated = true;
                            trackable.LastModifiedOnUtc = now;
                            trackable.LastModifierId = modifierId;
                            break;

                        case EntityState.Added:
                            trackable.LastModifierId = "0";
                            trackable.IsUpdated = false;
                            trackable.LastModifiedOnUtc = now;
                            break;

                        default: break;
                    }
                }
            }
        }

        private string GetLoggedInEmployee()
        {
            var httpContextAccessor = _httpContextAccessor.HttpContext;

            if (httpContextAccessor != null)
            {
                if (httpContextAccessor.User != null)
                {
                    var user = httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier);

                    if (user != null)
                    {
                        return user.Value;
                    }
                }
            }

            return string.Empty;
        }

        #endregion

        #region Overrides

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new User_Role_MiddleTable_Map());

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Props

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User_Role_MiddleTable> Users_Roles_MiddleTable { get; set; }

        #endregion
    }
}
