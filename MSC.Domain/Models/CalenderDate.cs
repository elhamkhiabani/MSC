using MSC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Domain.Models
{
    public class CalenderDate : IEntity
    {
        public CalenderDate()
        {

        }
        public int ID { get; set; }

        public string Date { get; set; }

        public int Year { get; set; }

        public int NumberOfMonth { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }

        public int NumberOfDay { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int ModifierID { get; set; }

        public DateTime ModifierDateTime { get; set; }
    }
}
