using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Models;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Checkins.Controllers
{
    [ApiController]
    [Route("checkins")]
    public class CheckinsController : ControllerBase
    {
        private readonly CheckinsContext _checkinsContext;

        public CheckinsController(CheckinsContext checkinsContext)
        {
            _checkinsContext = checkinsContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var checkins = await _checkinsContext.Checkins.ToListAsync();
            var checkinDtos = checkins.Select(c => new CheckinDto()
            {
                BeerName = c.BeerName,
                CheckinId = c.CheckinId,
                CreatedAt = c.CreatedAt,
                UserId = c.UserId
            });
            return Ok(checkinDtos);
        }

    }
}
