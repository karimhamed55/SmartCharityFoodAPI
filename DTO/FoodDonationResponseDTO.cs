namespace CharityApi.DTO
{
    public class FoodDonationResponseDTO
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public string DonorName { get; set; } // Flattened from Donor entity
        public string FoodType { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
    }
}