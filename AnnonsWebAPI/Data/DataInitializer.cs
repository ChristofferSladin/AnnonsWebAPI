using AnnonsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnonsWebAPI.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }

        private void SeedData()
        {
            if (!_dbContext.Ads
            .Any(e => e.Title == "Buy One, Get One Free!"))
            {
                _dbContext.Add(new Ad
                {
                    Title = "Buy One, Get One Free!",
                    Description = "Limited Time Offer",
                    TargetUrl = "https://example.com/shop",
                    StartDate = DateTime.Now,
                });
            }
        }

    }

}
