using System;
using System.Collections.Generic;

namespace Povorot.DAL.Models
{
    public class CarStation: BaseRecord
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime StartWorkTime { get; set; }
        public DateTime EndWorkTime { get; set; }
        public ICollection<RepairPost> RepairPosts { get; set; }
        public ICollection<Mechanic> Mechanics { get; set; }
    }
}
