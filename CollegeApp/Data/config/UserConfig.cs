using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_Learning.Data.config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).UseIdentityColumn();
            builder.Property(s => s.UserName).IsRequired();
            builder.Property(s => s.Password).IsRequired();
            builder.Property(s => s.PasswordSalt).IsRequired();
            builder.Property(s => s.IsActive).IsRequired();
            builder.Property(s => s.IsDeleted).IsRequired();
            builder.Property(s => s.UserTypeId).IsRequired();
            builder.Property(s => s.CreatedDate).IsRequired();
            builder.Property(s => s.ModifiedDate).IsRequired();
        }
    }
}
