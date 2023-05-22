namespace Lab.RateMyBeer.Ratings.Data.StarRatings
{
    public record StarRatingData(Guid Id, Guid CheckinId, int Rating, string Description);
   
}