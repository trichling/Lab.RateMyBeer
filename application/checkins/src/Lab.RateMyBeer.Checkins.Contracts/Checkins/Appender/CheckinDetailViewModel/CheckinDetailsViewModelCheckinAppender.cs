using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Framework.Composition.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinDetails;

namespace Lab.RateMyBeer.Checkins.Contracts.Checkins.Appender.CheckinDetailViewModel;

public class CheckinDetailsViewModelCheckinAppender : ViewModelAppenderBase<CheckinDetailsViewModel>
{
    private readonly ICheckinsRestApi _checkinsRestApi;

    public CheckinDetailsViewModelCheckinAppender(ICheckinsRestApi checkinsRestApi)
    {
        _checkinsRestApi = checkinsRestApi;
    }
    
    public override async Task<CheckinDetailsViewModel> AppendTo(CheckinDetailsViewModel viewModel, IViewModelCompositionContext context)
    {
        var checkin = (await _checkinsRestApi.GetByIds(new[] { viewModel.CheckinId })).Items.SingleOrDefault();

        if (checkin is null)
            return viewModel;

        viewModel.Checkin = new CheckinDetailsCheckinViewModel()
        {
            CheckinId = checkin.CheckinId,
            BeerName = checkin.BeerName,
            CreatedAt = checkin.CreatedAt,
            UserId = checkin.UserId
        };

        return viewModel;
    }
}