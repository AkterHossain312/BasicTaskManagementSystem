using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Constant;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Configurations
{
    public class TaskInfoDbConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable(TableNames.Tasks);
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(300);

            builder.Property(p => p.DueDate)
                .HasColumnType(DbDataType.Date);

            builder.Property(p => p.Status)
                .HasConversion<int>();

            builder
                .HasOne(t => t.User)         
                .WithMany(u => u.Tasks)       
                .HasForeignKey(t => t.UserId) 
                .IsRequired();

        }
    }
}
