using charityAPI.Models;
using CharityApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace charityAPI.Data
{
    public class DonorDbContext : DbContext
    {

        public DonorDbContext(DbContextOptions<DonorDbContext> options) : base(options)
        {

        }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<FoodDonation> FoodDonations { get; set; }
    }
}
