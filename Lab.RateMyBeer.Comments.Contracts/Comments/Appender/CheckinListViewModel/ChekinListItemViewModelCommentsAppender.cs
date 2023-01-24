

using apetito.Composition.ViewModels;
using Lab.RateMyBeer.Comments.Contracts.Comments.ApiClient;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinList;

namespace Lab.RateMyBeer.Comments.Contracts.Comments.Appender.CheckinListViewModel;

public class ChekinListViewModelCommentsAppender : ViewModelAppenderBase<CheckinListItemViewModel>
{
    private readonly ICommentsRestApi _commentsRestApi;

    public ChekinListViewModelCommentsAppender(ICommentsRestApi commentsRestApi)
    {
        _commentsRestApi = commentsRestApi;
    }


    public override async Task<CheckinListItemViewModel> AppendTo(CheckinListItemViewModel viewModel, IViewModelCompositionContext context)
    {
        var comment = (await _commentsRestApi.GetByCheckinIds(new[] { viewModel.CheckinId })).Items.SingleOrDefault();

        viewModel.Comment = new CheckinListItemCommentViewModel()
        {
            UserComment = comment?.UserComment ?? string.Empty
        };
        
        return viewModel;
    }

    public override async Task<IEnumerable<CheckinListItemViewModel>> AppendTo(IEnumerable<CheckinListItemViewModel> viewModel, IViewModelCompositionContext context)
    {
        var checkinListItemViewModels = viewModel.ToList();
        var ids = checkinListItemViewModels.Select(i => i.CheckinId);
        var comments = await _commentsRestApi.GetByCheckinIds(ids);

        foreach (var item in checkinListItemViewModels)
        {
            var comment = comments.Items.SingleOrDefault(c => c.CheckinId == item.CheckinId);
            item.Comment = new CheckinListItemCommentViewModel()
            {
                UserComment = comment?.UserComment ?? string.Empty
            };
        }

        return checkinListItemViewModels;
    }
}