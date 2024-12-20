using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    public class UpdateViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Month { get; set; }

        public int? BasicSalary { get; set; }

        public int? Allowance { get; set; }

        public int? Transportation { get; set; }

        public string? OverTimeCalculatorMethod { get; set; }

    }
}
