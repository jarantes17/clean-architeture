using BPDotNet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPDotNet.Data.Mappings
{
    public class UserMap: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();

            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(50).IsRequired();

            builder.Property(x => x.Password).HasColumnName("password").IsRequired();

            builder.Property(x => x.CreatedAt).HasColumnName("created_at");

            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
        }
    }
}