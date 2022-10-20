using Lab.RateMyBeer.Comments.Contracts.Comments.Models;
using RestEase;

namespace Lab.RateMyBeer.Comments.Contracts.Comments.ApiClient
{
    public interface ICommentsRestApi
    {
        [Get("comments")]
        public Task<IEnumerable<CommentsDto>> GetByCheckinIds(IEnumerable<Guid> checkinIds);
    }
}