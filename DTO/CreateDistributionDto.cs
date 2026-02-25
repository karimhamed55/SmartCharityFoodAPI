namespace CharityApi.DTO
{
    public class CreateDistributionDto
    {
        public int FoodDonationId { get; set; }
        public string TargetName { get; set; }
        public int RequestedQuantity { get; set; }
    }
}
