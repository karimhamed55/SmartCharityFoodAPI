using System.ComponentModel.DataAnnotations;

namespace charityAPI.DTO
{
    public class DonorPostDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string City { get; set; }
    }
}
