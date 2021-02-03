using System;
using System.Collections.Generic;

namespace Povorot.DAL.Models
{
    public class Request: BaseRecord
    {
        public long CarStationId { get; set; }
        public CarStation CarStation { get; set; }
        public long? RepairPostId { get; set; }
        public RepairPost RepairPost { get; set; }
        public long? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
        public DateTime Time { get; set; }
        public long ClientId { get; set; }
        public User Client { get; set; }
        public string PhoneNumber { get; set; }
        public long ClientCarId { get; set; }
        public ClientCar ClientCar { get; set; }
        public ICollection<Work> Works { get; set; }
        public string Description { get; set; }
    }
}