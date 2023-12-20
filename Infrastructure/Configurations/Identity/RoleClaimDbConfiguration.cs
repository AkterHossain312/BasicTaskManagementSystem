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
    public class RoleClaimDbConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable(TableNames.RoleClaims);
        }
    }
}
