using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Models;
using RestEase;

namespace Lab.RateMyBeer.Ratings.Contracts.StarRatings;

public interface IRatingsRestApi
{
    [Get("ratings")]
    public Task<IEnumerable<StarRatingDto>> GetByCheckinIds(IEnumerable<Guid> checkinIds);
}