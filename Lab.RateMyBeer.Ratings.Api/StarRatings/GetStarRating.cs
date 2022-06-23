using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Models;
using Lab.RateMyBeer.Ratings.Data.StarRatings;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Ratings.Api.StarRatings
{
    public static class GetStarRating
    {
        public static async Task<IResult> Handle(StarRatingContext context, Guid checkinIds)
        {
            var rating = context.StarRatings.Single(r => r.CheckinId == checkinIds);

            var ratingDto = new StarRatingDto(rating.Id, rating.CheckinId, rating.Rating, rating.Description);

            return Results.Ok(ratingDto);
        }
    
    }
}