namespace Povorot.DAL.Models
{
    public class RepairPost: BaseRecord
    {
        public string Name { get; set; }
        public long CarStationId { get; set; }
        public CarStation CarStation { get; set; }
    }
}