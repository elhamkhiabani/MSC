
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
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.ToTable("Salaries");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(500);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(500);
            builder.Property(x => x.BasicSalary);
            builder.Property(x => x.Transportation);
            builder.Property(x => x.Allowance);
            builder.Property(x => x.SalaryAmount);
            builder.Property(x => x.Date).HasMaxLength(10);
            builder.Property(x => x.Time).HasMaxLength(10);
            builder.Property(x=>x.OverTimeCalculatorMethod).IsRequired();
            builder.Property(x => x.IsActive);
            builder.Property(x => x.IsDelete);
            builder.Property(x=>x.CreationDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.CreatorID);
            builder.Property(x => x.ModifierDateTime).IsRequired(false);
            builder.Property(x => x.ModifierID);
        }
    }
}
