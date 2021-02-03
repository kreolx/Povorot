using System.ComponentModel.DataAnnotations;

namespace Povorot.Api.Dto
{
    public class MechanicDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phonenumber { get; set; }
        
        public IdNameDto CarStation { get; set; }
    }

    public record MechanicCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Проверьте правильность ввода номера телефона")]
        public string Phonenumber { get; set; }
        [Required]
        public long CarStationId { get; set; }
    }
}
