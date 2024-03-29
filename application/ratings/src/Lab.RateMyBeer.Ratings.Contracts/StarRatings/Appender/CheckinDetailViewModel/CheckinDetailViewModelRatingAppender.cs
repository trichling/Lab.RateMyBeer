﻿using Lab.RateMyBeer.Framework.Composition.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinDetails;

namespace Lab.RateMyBeer.Ratings.Contracts.StarRatings.Appender.CheckinDetailViewModel;

public class CheckinDetailViewModelRatingAppender : ViewModelAppenderBase<CheckinDetailsViewModel>
{
    private readonly IRatingsRestApi _ratingsRestApi;

    public CheckinDetailViewModelRatingAppender(IRatingsRestApi ratingsRestApi)
    {
        _ratingsRestApi = ratingsRestApi;
    }
    
    public override async Task<CheckinDetailsViewModel> AppendTo(CheckinDetailsViewModel viewModel, IViewModelCompositionContext context)
    {
        var rating = (await _ratingsRestApi.GetByCheckinIds(new[] { viewModel.CheckinId })).Items.SingleOrDefault();

        if (rating is null)
            return viewModel;

        viewModel.Rating = new CheckinDetailsRatingViewModel()
        {
            StarRating = rating.Rating,
            RatingCategory = rating.Description
        };

        return viewModel;
    }
}