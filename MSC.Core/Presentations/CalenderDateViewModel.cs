using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    public class CalenderDateViewModel
    {
        public int ID { get; set; }

        public string Date { get; set; }

        public int Year { get; set; }

        public int NumberOfMonth { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }

        public int NumberOfDay { get; set; }

        public bool IsActive { get; set; }
    }
}
