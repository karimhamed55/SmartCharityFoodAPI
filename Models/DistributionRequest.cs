using System.ComponentModel.DataAnnotations.Schema;

namespace CharityApi.Models
{
    public class DistributionRequest
    {
        public int Id { get; set; }
        [ForeignKey("FoodDonation")]
        public int FoodDonationId { get; set; }
        public FoodDonation FoodDonation { get; set; }

        public string TargetName { get; set; }

        public int RequestedQuantity { get; set; }

        public string Status { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}
