using charityAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityApi.Models
{
    public class FoodDonation
    {
        public int Id { get; set; }

        [Required]
        public int DonorId { get; set; }

        [ForeignKey("DonorId")]
        public Donor Donor { get; set; }

        [Required]
        public string FoodType { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Status { get; set; } = "Available";
        public ICollection<DistributionRequest> DistributionRequests { get; set; }
    }
}
