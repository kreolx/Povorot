using System.ComponentModel.DataAnnotations;

namespace Povorot.Api.Dto
{
    public record IdNameDto
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
