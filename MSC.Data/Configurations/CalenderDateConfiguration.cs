using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Data.Configurations
{
    public class CalenderDateConfiguration : IEntityTypeConfiguration<CalenderDate>
    {
        public void Configure(EntityTypeBuilder<CalenderDate> builder)
        {
            builder.ToTable("CalenderDates");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Date).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Year);
            builder.Property(x => x.Day).IsRequired().HasMaxLength(25);
            builder.Property(x => x.NumberOfDay);
            builder.Property(x => x.Month).HasMaxLength(25);
            builder.Property(x => x.NumberOfMonth);
            builder.Property(x => x.IsActive);
            builder.Property(x => x.IsDelete);
            builder.Property(x => x.CreationDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.CreatorID);
            builder.Property(x => x.ModifierDateTime).IsRequired(false);
            builder.Property(x => x.ModifierID);
        }
    }
}
