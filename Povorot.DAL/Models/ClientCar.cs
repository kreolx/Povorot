using System;

namespace Povorot.DAL.Models
{
    public class ClientCar: BaseRecord
    {
        public long CarId { get; set; }
        public Car Car { get; set; }
        public decimal Mileage { get; set; }
    }
}