namespace Povorot.Api.Dto
{
    public record RepairPostDto: RepairPostCreateDto
    {
        public long Id { get; set; }
        public IdNameDto CarStation { get; set; }
    }

    public record RepairPostCreateDto
    {
        public string Name { get; set; }
    }
}