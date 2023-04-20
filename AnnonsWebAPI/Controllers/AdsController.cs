using AnnonsWebAPI.Data;
using AnnonsWebAPI.Models;
using AnnonsWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnonsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AdsController(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdDTO>>> GetAll()
        {
            var ads = await _dbContext.Ads.ToListAsync();
            var adDTOs = ads.Select(ad => new AdDTO
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                TargetUrl = ad.TargetUrl,
                StartDate = DateTime.Now
            }).ToList();

            return Ok(adDTOs);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AdDTO>> GetOne(int id)
        {
            var ad = _dbContext.Ads.Find(id);
            
            if (ad == null)
            {
                return BadRequest("Ad not found");
            }

            var adDTO = new AdDTO
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                TargetUrl= ad.TargetUrl,
                StartDate = DateTime.Now
            };

            return Ok(adDTO);
        }

        [HttpPost]
        public async Task<ActionResult<AdDTO>> PostAd(UpdateAdDTO adDTO)
        {
            var ad = new Ad
            {
                Title = adDTO.Title,
                Description = adDTO.Description,
                TargetUrl = adDTO.TargetUrl,
                StartDate = DateTime.Now,
            };

            _dbContext.Ads.Add(ad);
            await _dbContext.SaveChangesAsync();

            return Ok(adDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<AdDTO>> UpdateAd(UpdateAdDTO updateAdDTO, int id)
        {
            var adToUpdate = _dbContext.Ads.Find(id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            adToUpdate.Title = updateAdDTO.Title;
            adToUpdate.Description = updateAdDTO.Description;
            adToUpdate.TargetUrl = updateAdDTO.TargetUrl;
            adToUpdate.StartDate = updateAdDTO.StartDate;

            await _dbContext.SaveChangesAsync();

            return Ok(updateAdDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Ad>> Delete(int id)
        {
            var ad = _dbContext.Ads.Find(id);

            if (ad == null)
            {
                return BadRequest("Ad not found");
            }

            _dbContext.Ads.Remove(ad);
            await _dbContext.SaveChangesAsync();

            return Ok("Ad Deleted Successfully");
        }
    }
}
