using Lab.RateMyBeer.Comments.Contracts.Comments.ApiClient;
using Lab.RateMyBeer.Framework.Composition.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinDetails;

namespace Lab.RateMyBeer.Comments.Contracts.Comments.Appender.CheckinDetailViewModel;

public class CheckinDetailViewModelCommentsAppender : ViewModelAppenderBase<CheckinDetailsViewModel>
{
    private readonly ICommentsRestApi _commentsRestApi;

    public CheckinDetailViewModelCommentsAppender(ICommentsRestApi commentsRestApi)
    {
        _commentsRestApi = commentsRestApi;
    }
    
    public override async Task<CheckinDetailsViewModel> AppendTo(CheckinDetailsViewModel viewModel, IViewModelCompositionContext context)
    {
        var comment = (await _commentsRestApi.GetByCheckinIds( new [] { viewModel.CheckinId })).Items.SingleOrDefault();

        if (comment is null)
            return viewModel;

        viewModel.Comments = new CheckinDetailsCommentsViewModel()
        {
            UserComment = comment.UserComment,
            BreweryComment = comment.BreweryComment,
            Comments = new CheckinDetailsCommentListViewModel()
            {
                Items = comment.Comments.Select(c => new CheckinDetailsCommentListItemViewModel()
                {
                    UserId = c.UserId,
                    Comment = c.Comment
                }).ToList()
            }
        };

        return viewModel;
    }
}