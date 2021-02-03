namespace Povorot.DAL.Models
{
    public class CarModel: BaseRecord
    {
        public string Name { get; set; }
        public long CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }
    }
}