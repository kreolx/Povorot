namespace Povorot.DAL.Models
{
    public class Work: BaseRecord
    {
        public string Name { get; set; }
        public long WorkCategoryId { get; set; }
        public WorkCategory WorkCategory { get; set; }
    }
}