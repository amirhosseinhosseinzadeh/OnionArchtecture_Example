using Microsoft.EntityFrameworkCore;
using Onion.Data.Mappings;
using Onion.Domain.Security;
using Onion.Libraries.Domain.Security;
using Onion.Libraries.Domain.Users;

namespace Onnion.Data
{
    public class ObjectContext : DbContext 
    {
        #region Ctor

        public ObjectContext(DbContextOptions options) : base(options)
        {
        }

        #endregion

        #region Utils

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