namespace Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Commands
{
    public record CreateStarRatingCommand(Guid RatingId, Guid CheckinId, Guid UserId, int Rating);
}