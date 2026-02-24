using System.ComponentModel.DataAnnotations;

namespace charityAPI.Models
{
    public class Donor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Phone { get; set; }
        public string City { get; set; } = string.Empty;

        public DateTime CreatedAT { get; set; }
    }
}
