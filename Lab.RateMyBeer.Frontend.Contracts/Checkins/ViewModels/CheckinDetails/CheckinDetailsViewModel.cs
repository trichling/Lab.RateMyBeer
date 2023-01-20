using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;

public class CheckinDetailsViewModel
{
    public Guid CheckinId { get; set; }

    public CheckinDetailsCheckinViewModel CheckinDetailsCheckin { get; set; }
    public CheckinDetailsRatingViewModel CheckinDetailsRating { get; set; }
    public CheckinDetailsCommentsViewModel CheckinDetailsComments { get; set; }

}