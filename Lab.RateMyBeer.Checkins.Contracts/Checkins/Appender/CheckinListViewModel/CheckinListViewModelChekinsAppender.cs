using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apetito.Composition.ViewModels;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinList;

namespace Lab.RateMyBeer.Checkins.Contracts.Checkins.Appender.CheckinListViewModel;

public class CheckinListViewModelCheckinsAppender : ViewModelAppenderBase<Frontend.Contracts.Checkins.ViewModels.CheckinList.CheckinListViewModel>
{
    private readonly ICheckinsRestApi _checkinsRestApi;

    public CheckinListViewModelCheckinsAppender(ICheckinsRestApi checkinsRestApi)
    {
        _checkinsRestApi = checkinsRestApi;
    }

    public override async Task<Frontend.Contracts.Checkins.ViewModels.CheckinList.CheckinListViewModel> AppendTo(Frontend.Contracts.Checkins.ViewModels.CheckinList.CheckinListViewModel viewModel, IViewModelCompositionContext context)
    {
        // Additional parameters could be grabbed from the context
        context.TryGetValue<int>("page", out var page);
     
        var checkins = await _checkinsRestApi.GetAll();
        viewModel.Items = checkins.Items.Select(c => new CheckinListItemViewModel()
        {
            CheckinId = c.CheckinId, 
            Checkin = new CheckinListItemCheckinViewModel()
            {
                UserId = c.UserId,
                CreatedAt = c.CreatedAt,
                BeerName = c.BeerName
            }
        }).ToList();

        viewModel.Items = (await context.ComposeList<CheckinListItemViewModel>(viewModel.Items)).ToList();
        
        return viewModel;
    }
}