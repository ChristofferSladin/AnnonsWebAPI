using AnnonsWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnonsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ILogger<AdsController> _logger;

        public AdsController(ILogger<AdsController> logger)
        {
            _logger = logger;
        }

        private static List<Ad> AdList = new List<Ad>
        {
            new Ad
            {
                Id = 1,
            Title = "Buy One, Get One Free!",
            Description = "Shop now and get a free item with your purchase!",
            TargetUrl = "https://example.com/shop",
            StartDate = new DateTime(2023, 5, 1)
            },

            new Ad
            {
                Id = 2,
            Title = "Buy One, Get Two Free!",
            Description = "Shop now and get a free item with your purchase!",
            TargetUrl = "https://example.com/shop",
            StartDate = new DateTime(2023, 5, 1)
            },

            new Ad
            {
                Id = 3,
            Title = "Buy One, Get One Three Free!",
            Description = "Shop now and get a free item with your purchase!",
            TargetUrl = "https://example.com/shop",
            StartDate = new DateTime(2023, 5, 1)
            },

            new Ad
            {
                Id = 4,
            Title = "Buy One, Get One Free!",
            Description = "Shop now and get a free item with your purchase!",
            TargetUrl = "https://example.com/shop",
            StartDate = new DateTime(2023, 5, 1)
            },

            new Ad
            {
                Id = 5,
            Title = "Buy One, Get Two!",
            Description = "Shop now and get a free item with your purchase!",
            TargetUrl = "https://example.com/shop",
            StartDate = new DateTime(2023, 5, 1)
            },

        };

        [HttpGet]
        public async Task<ActionResult<List<Ad>>> GetAll()
        {
            return Ok(AdList);
        }
        

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Ad>> GetOne(int id)
        {
            var ad = AdList.Find(s => s.Id == id);

            if (ad == null)
            {
                return BadRequest("Ad not found");
            }
            return Ok(ad);
        }

        [HttpPost]
        public async Task<ActionResult<Ad>> PostHero(Ad ad)
        {
            AdList.Add(ad);
            return Ok(AdList);
        }

        [HttpPut]
        public async Task<ActionResult<Ad>> UpdateHero(Ad ad)
        {
            // OBS: PUT Uppdaterar HELA SuperHero (ALLA properties)
            var adToUpdate = AdList.Find(s => s.Id == ad.Id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            adToUpdate.Title = ad.Title;
            adToUpdate.Description = ad.Description;
            adToUpdate.TargetUrl = ad.TargetUrl;
            adToUpdate.StartDate = ad.StartDate;

            return Ok(AdList);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Ad>> Delete(int id)
        {
            var hero = AdList.Find(s => s.Id == id);

            if (hero == null)
            {
                return BadRequest("Ad not found");
            }

            AdList.Remove(hero);
            return Ok(AdList);
        }



    }
}
