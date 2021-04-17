using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Domain.Security;

namespace Onion.Data.Mappings
{
    public class User_Role_MiddleTable_Map : IBaseEntityTypeConfiguration<User_Role_MiddleTable>
    {
        public void Configure(EntityTypeBuilder<User_Role_MiddleTable> builder)
        {
            builder.ToTable("User_Role_MiddleTable");
            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Role)
                .WithMany(role => role.Users_Roles_MiddleTable)
                .HasForeignKey(mapping => mapping.RoleId)
                .IsRequired();

            builder.HasOne(prop => prop.User)
                .WithMany(user => user.User_Role_MiddleTable)
                .HasForeignKey(mapping => mapping.UserId)
                .IsRequired();
        }
    }
}
