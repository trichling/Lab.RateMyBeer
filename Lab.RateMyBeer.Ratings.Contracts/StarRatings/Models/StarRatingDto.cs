namespace Lab.RateMyBeer.Ratings.Contracts.StarRatings.Models
{
    public record StarRatingDto(Guid Id, Guid CheckinId, int Rating, string Description);
    
}