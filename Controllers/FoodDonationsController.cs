using charityAPI.Data;
using CharityApi.DTO;
using CharityApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityApi.Controllers
{
    [Route("api/food-donations")]
    [ApiController]
    public class FoodDonationsController : ControllerBase
    {
        private readonly DonorDbContext _context;

        public FoodDonationsController(DonorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodDonationCreateDTO dto)
        {
            var donorExists = await _context.Donors.AnyAsync(d => d.Id == dto.DonorId);

            if (!donorExists)
                return NotFound("Donor not found");

            if (dto.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero");

            var donation = new FoodDonation
            {
                DonorId = dto.DonorId,
                FoodType = dto.FoodType,
                Quantity = dto.Quantity,
                ExpiryDate = dto.ExpiryDate,
                Status = "Available"
            };

            _context.FoodDonations.Add(donation);
            await _context.SaveChangesAsync();

            var response = new FoodDonationResponseDTO
            {
                Id = donation.Id,
                DonorId = donation.DonorId,
                FoodType = donation.FoodType,
                Quantity = donation.Quantity,
                ExpiryDate = donation.ExpiryDate,
                Status = donation.Status
            };

            return CreatedAtAction(nameof(GetById), new { id = donation.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donations = await _context.FoodDonations
                .Include(d => d.Donor)
                .Select(d => new FoodDonationResponseDTO
                {
                    Id = d.Id,
                    DonorId = d.DonorId,
                    DonorName = d.Donor != null ? d.Donor.Name : "Unknown",
                    FoodType = d.FoodType,
                    Quantity = d.Quantity,
                    ExpiryDate = d.ExpiryDate,
                    Status = d.Status
                })
                .ToListAsync();

            return Ok(donations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var d = await _context.FoodDonations
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (d == null)
                return NotFound("Donation not found");

            var response = new FoodDonationResponseDTO
            {
                Id = d.Id,
                DonorId = d.DonorId,
                DonorName = d.Donor != null ? d.Donor.Name : "Unknown",
                FoodType = d.FoodType,
                Quantity = d.Quantity,
                ExpiryDate = d.ExpiryDate,
                Status = d.Status
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FoodDonationCreateDTO dto)
        {
            var donation = await _context.FoodDonations.FindAsync(id);

            if (donation == null)
                return NotFound("Donation not found");

            donation.DonorId = dto.DonorId;
            donation.FoodType = dto.FoodType;
            donation.Quantity = dto.Quantity;
            donation.ExpiryDate = dto.ExpiryDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.FoodDonations.FindAsync(id);

            if (donation == null)
                return NotFound("Donation not found");

            _context.FoodDonations.Remove(donation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}