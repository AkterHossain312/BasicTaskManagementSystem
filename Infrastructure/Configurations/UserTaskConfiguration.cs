using Domain.Models;
using Infrastructure.Constant;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.ToTable(TableNames.UserTask);
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.TaskId)
                .IsRequired();

            builder.HasKey(x => new { x.TaskId, x.UserId });

            builder.HasOne(x=>x.User).WithMany(x=>x.UsertTasks).HasForeignKey(x=>x.UserId);
            builder.HasOne(x=>x.Tasks).WithMany(x=>x.UserTasks).HasForeignKey(x=>x.TaskId);



        }
    }
}
