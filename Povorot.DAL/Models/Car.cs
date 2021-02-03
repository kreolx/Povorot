namespace Povorot.DAL.Models
{
    public class Car: BaseRecord
    {
        public long CarModelId { get; set; }
        public CarModel CarModel { get; set; }
        public decimal Weight { get; set; }
        public long EngineId { get; set; }
        public Engine Engine { get; set; }
        public long TransmissionId { get; set; }
        public Transmission Transmission { get; set; }
        public long CarTypeId { get; set; }
        public CarType CarType { get; set; }
        public string BodyNumber { get; set; }
        public string OilFilter { get; set; }
        public string AirFilter { get; set; }
    }
}