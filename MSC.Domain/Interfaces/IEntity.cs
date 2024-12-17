using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Domain.Interfaces
{
    public interface IEntity
    {
        public int ID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int ModifierID { get; set; }

        public DateTime ModifierDateTime { get; set; }
    }
}
