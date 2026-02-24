namespace CharityApi.DTO
{
    public class FoodDonationCreateDTO
    {
        public int DonorId { get; set; }

        public string FoodType { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
