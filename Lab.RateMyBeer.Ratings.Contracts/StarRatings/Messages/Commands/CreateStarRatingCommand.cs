namespace Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Commands
{
    public record CreateStarRatingCommand(Guid CheckinId, int Rating);
}