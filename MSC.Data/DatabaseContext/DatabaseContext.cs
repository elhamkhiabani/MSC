using Microsoft.EntityFrameworkCore;
using MSC.Data.Configurations;
using MSC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Data.DatabseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SalaryConfiguration());
        }

        public DbSet<Salary> Salaries { get; set; }
        public DbSet<CalenderDate> CalenderDates { get; set; }


    }
}
