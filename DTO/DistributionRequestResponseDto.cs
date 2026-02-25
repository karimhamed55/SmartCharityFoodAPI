namespace CharityApi.DTO
{
    public class DistributionRequestResponseDto
    {
        public int Id { get; set; }
        public int FoodDonationId { get; set; }
        public string TargetName { get; set; }
        public int RequestedQuantity { get; set; }
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public string FoodType { get; set; } // Just take the name, not the whole object
    }
}