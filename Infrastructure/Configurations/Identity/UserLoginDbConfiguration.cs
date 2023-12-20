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
    public class UserLoginDbConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(p => p.ProviderKey).HasMaxLength(128);
            builder.Property(p => p.LoginProvider).HasMaxLength(128);
            builder.ToTable(TableNames.UserLogins);
        }
    }
}