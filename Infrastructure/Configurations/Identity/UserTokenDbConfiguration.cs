using Domain.Models.Identity;
using Infrastructure.Constant;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Identity
{
    public class UserTokenDbConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            // Composite key
            builder.HasKey(p => new { p.UserId, p.LoginProvider, p.Name });
            builder.Property(p => p.LoginProvider).HasMaxLength(128);
            builder.Property(p => p.Name).HasMaxLength(128);
            builder.ToTable(TableNames.UserTokens);
        }
    }
}