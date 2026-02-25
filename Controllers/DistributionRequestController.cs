using charityAPI.Data;
using charityAPI.Models;
using CharityApi.DTO;
using CharityApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CharityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionRequestController : ControllerBase
    {
        private readonly DonorDbContext _context;
        public DistributionRequestController(DonorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDistributionDto dto)
        {
            var donation = await _context.FoodDonations
                .FirstOrDefaultAsync(x => x.Id == dto.FoodDonationId);
            if (donation == null) 
                return NotFound("Donation not found");

            if (dto.RequestedQuantity > donation.Quantity)
                return BadRequest("Requested quantity exceeds available quantity");

            if(donation.Status != "Available") 
                return BadRequest("Donation does not allow distribution");

            var request = new DistributionRequest
            {
                FoodDonationId = dto.FoodDonationId,
                TargetName = dto.TargetName,
                RequestedQuantity = dto.RequestedQuantity,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow
            };
           _context.DistributionRequests.Add(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDistributionRequest()
        {
            var requests = await _context.DistributionRequests
                .Include(x => x.FoodDonation)
                .ToListAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request =await _context.DistributionRequests
                .Include(x=>x.FoodDonation)
                .FirstOrDefaultAsync(x=>x.Id == id);

            if(request == null) 
                return NotFound("Request not found");

            return Ok(request);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id,UpdateStatusDto dto)
        {
            var request = await _context.DistributionRequests.FindAsync(id);

            if (request == null)
                return NotFound("Request not found");

            request.Status = dto.Status;

            if (dto.Status == "Completed")
            {
                var donation = await _context.FoodDonations
                    .FirstOrDefaultAsync(x => x.Id == request.FoodDonationId);

                if (donation != null)
                    donation.Quantity -= request.RequestedQuantity;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
