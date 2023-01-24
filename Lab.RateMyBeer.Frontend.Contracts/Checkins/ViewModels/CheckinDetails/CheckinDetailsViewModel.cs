using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinDetails;

public class CheckinDetailsViewModel
{
    public Guid CheckinId { get; set; }

    public CheckinDetailsCheckinViewModel Checkin { get; set; }
    public CheckinDetailsRatingViewModel Rating { get; set; }
    public CheckinDetailsCommentsViewModel Comments { get; set; }

}