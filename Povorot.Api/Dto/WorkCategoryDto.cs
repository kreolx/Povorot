using System.ComponentModel.DataAnnotations;

namespace Povorot.Api.Dto
{
    public record WorkCategoryDto: WorkCategoryCreateDto
    {
        public long Id { get; set; }
    }

    public record WorkCategoryCreateDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Название категории обязательное поле")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
