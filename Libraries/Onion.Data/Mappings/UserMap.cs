using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Domain.Users;

namespace Onion.Data.Mappings
{
    public class UserMap : IBaseEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .HasKey(prop => prop.Id);

            builder.Property(nameof(User.UserName)).IsRequired();
            builder.Property(nameof(User.EmailAddress)).IsRequired();
            builder.Property(nameof(User.Password)).IsRequired();
            builder.Property(nameof(User.Key)).HasMaxLength(5);
        }
    }
}
