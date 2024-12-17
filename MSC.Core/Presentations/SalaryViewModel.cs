using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    public class SalaryViewModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BasicSalary { get; set; }

        public int Allowance { get; set; }

        public int Transportation { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string OverTimeCalculatorMethod { get; set; }

        public int SalaryAmount { get; set; }

        public bool IsActive { get; set; }
    }
}
