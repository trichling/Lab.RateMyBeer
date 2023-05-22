using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Models;
using Lab.RateMyBeer.Ratings.Data.StarRatings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Ratings.Api.StarRatings
{
    public static class GetStarRatings
    {
        public static async Task<IResult> Handle([FromQuery] CheckinIds checkinIds, [FromServices]StarRatingContext context)
        {
            var ratings = await context.StarRatings.Where(r => checkinIds.Contains(r.CheckinId)).ToListAsync();

            var ratingDtos = ratings.Select(c => new StarRatingDto(c.Id, c.CheckinId, c.Rating, c.Description)).ToList();

            return Results.Ok(new StarRatingsByCheckinIdsDto(ratingDtos));
        }
    
    }
}