using BPDotNet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPDotNet.Infrastructure.Persistence.Mappings
{
    public class UserMap: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Password).IsRequired().HasDefaultValue("secret");
        }
        
    }
}