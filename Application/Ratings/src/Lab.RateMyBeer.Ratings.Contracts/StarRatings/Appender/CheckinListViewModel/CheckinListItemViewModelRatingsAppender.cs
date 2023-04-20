using Lab.RateMyBeer.Framework.Composition.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinList;

namespace Lab.RateMyBeer.Ratings.Contracts.StarRatings.Appender.CheckinListViewModel;

public class CheckinListItemViewModelRatingsAppender : ViewModelAppenderBase<CheckinListItemViewModel>
{
    private readonly IRatingsRestApi _ratingsRestApi;

    public CheckinListItemViewModelRatingsAppender(IRatingsRestApi ratingsRestApi)
    {
        _ratingsRestApi = ratingsRestApi;
    }
    
    public override Task<CheckinListItemViewModel> AppendTo(CheckinListItemViewModel viewModel, IViewModelCompositionContext context)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<CheckinListItemViewModel>> AppendTo(IEnumerable<CheckinListItemViewModel> viewModel, IViewModelCompositionContext context)
    {
        var checkinListItemViewModels = viewModel.ToList();
        var ids = checkinListItemViewModels.Select(i => i.CheckinId);
        var ratings = await _ratingsRestApi.GetByCheckinIds(ids);

        foreach (var item in checkinListItemViewModels)
        {
            var rating = ratings.Items.SingleOrDefault(c => c.CheckinId == item.CheckinId);
            item.Rating = new CheckinListItemRatingViewModel()
            {
                RatingCategory = rating?.Description ?? string.Empty 
            };
        }

        return checkinListItemViewModels;
    }
}