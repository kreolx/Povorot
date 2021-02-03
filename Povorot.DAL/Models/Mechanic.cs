namespace Povorot.DAL.Models
{
    public class Mechanic: BaseRecord
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public long CarStationId { get; set; }
        public CarStation CarStation { get; set; }
    }
}
