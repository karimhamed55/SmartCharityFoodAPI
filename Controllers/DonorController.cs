using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using charityAPI.Data;
using charityAPI.Models;
using charityAPI.DTO;

namespace charityAPI.Controllers
{
    [Route("api/donors")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly DonorDbContext _context;
        public DonorsController(DonorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Donor>> CreateDonor([FromBody]DonorPostDTO donorDto)
        {
            var donor = new Donor
            {
                Name = donorDto.Name,
                Phone = donorDto.Phone,
                City = donorDto.City,
                CreatedAT = DateTime.UtcNow
            };

            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDonorById), new { id = donor.Id }, donor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetAllDonors([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return await _context.Donors
                .OrderByDescending(d => d.CreatedAT)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetDonorById(int id)
        {
            var donor = await _context.Donors.FindAsync(id);

            if (donor == null) return NotFound();

            return Ok(donor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonor(int id, DonorPostDTO donorDto)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null) return NotFound();

            donor.Name = donorDto.Name;
            donor.Phone = donorDto.Phone;
            donor.City = donorDto.City;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null) return NotFound();

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}