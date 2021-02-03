namespace Povorot.DAL.Models
{
    public class Price: BaseRecord
    {
        public long CarStationId { get; set; }
        public CarStation CarStation { get; set; }
        public long WorkId { get; set; }
        public Work Work { get; set; }
        public long CarTypeId { get; set; }
        public CarType CarType { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
    }
}