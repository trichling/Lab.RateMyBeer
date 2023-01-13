using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Models;
using RestEase;

namespace Lab.RateMyBeer.Ratings.Contracts.StarRatings;

public interface IRatingsRestApi
{
    [Get("ratings")]
    public Task<StarRatingsByCheckinIdsDto> GetByCheckinIds(IEnumerable<Guid> checkinIds);
}