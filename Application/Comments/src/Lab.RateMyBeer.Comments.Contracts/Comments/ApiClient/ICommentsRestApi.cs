using Lab.RateMyBeer.Comments.Contracts.Comments.Models;
using RestEase;

namespace Lab.RateMyBeer.Comments.Contracts.Comments.ApiClient
{
    public interface ICommentsRestApi
    {
        [Get("comments")]
        public Task<CommentsByCheckinIdsDto> GetByCheckinIds(IEnumerable<Guid> checkinIds);
    }
}