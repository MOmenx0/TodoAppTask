using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {

        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Description)
                   .HasMaxLength(500); 

            builder.Property(t => t.Status)
                   .IsRequired();

            builder.Property(t => t.Priority)
                   .IsRequired();

            builder.Property(t => t.DueDate)
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.Property(t => t.CreatedDate)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(t => t.LastModifiedDate)
                   .HasColumnType("datetime")
                   .IsRequired();
        }
    }
}
