using charityAPI.Data;
using CharityApi.DTO;
using CharityApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityApi.Controllers
{
    [Route("api/[controller]")]
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

            return CreatedAtAction(nameof(GetById), new { id = donation.Id }, donation);
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donations = await _context.FoodDonations
                .Include(d => d.Donor)
                .ToListAsync();

            return Ok(donations);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donation = await _context.FoodDonations
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null)
                return NotFound("Donation not found");

            return Ok(donation);
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

            return Ok(donation);
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
