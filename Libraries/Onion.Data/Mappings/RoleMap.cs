using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Libraries.Domain.Security;

namespace Onion.Data.Mappings
{
    public class RoleMap : IBaseEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(prop => prop.Id);
            builder.Property(nameof(Role.RoleName))
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
