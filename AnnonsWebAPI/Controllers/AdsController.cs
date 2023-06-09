﻿using AnnonsWebAPI.Data;
using AnnonsWebAPI.Models;
using AnnonsWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AnnonsWebAPI.Controllers
{
    [EnableCors("AllowAll")]

    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AdsController(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        // READ ALL ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ALL Ads from the database
        /// </summary>
        /// <returns>
        /// A full list of ALL Ads
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/Ads
        /// </remarks>
        /// <response code="200">
        /// Successfully returned a full list of ALL Ads
        /// </response>

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
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

        // READ ONE ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ONE Ad from the database
        /// </summary>
        /// <returns>
        /// One Ad
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned ONE Ad
        /// </response>

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
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
                TargetUrl = ad.TargetUrl,
                StartDate = DateTime.Now
            };

            return Ok(adDTO);
        }

        // CREATE AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Create an Ad and save it in the database
        /// </summary>
        /// <returns>
        /// A new create Ad
        /// </returns>
        /// <remarks>
        /// Example end point: POST /api/Ads
        /// </remarks>
        /// <response code="200">
        /// Successfully returned a created Ad
        /// </response>

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        // UPDATE AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Update an Ad and save the changes in the database
        /// </summary>
        /// <returns>
        /// An updated Ad
        /// </returns>
        /// <remarks>
        /// Example end point: PUT /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned an updated Ad
        /// </response>

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
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

        // UPDATE A PARTIAL OF AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Update a partial of an Ad and save the changes in the database
        /// </summary>
        /// <returns>
        /// A partial of the updated Ad
        /// </returns>
        /// <remarks>
        /// Example end point: PATCH /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned the partial of the updated Ad
        /// </response>

        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UpdateAdDTO>> PatchAd(JsonPatchDocument ad, int id)
        {
            var adToUpdate = await
                _dbContext.Ads.FindAsync(id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            ad.ApplyTo(adToUpdate);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Ads.ToListAsync());

        }

        // DELETE AN AD ///////////////////////////////////////////////////////
        /// <summary>
        /// Delete an Ad and remove it from the database
        /// </summary>
        /// <returns>
        /// Deletes the AD
        /// </returns>
        /// <remarks>
        /// Example end point: DELETE /api/Ads{id}
        /// </remarks>
        /// <response code="200">
        /// Ad deleted successfully
        /// </response>

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
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
