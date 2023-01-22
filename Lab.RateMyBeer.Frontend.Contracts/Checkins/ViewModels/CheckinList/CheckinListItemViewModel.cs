using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinList
{
    public class CheckinListItemViewModel
    {
        public Guid CheckinId { get; set; }
        
        public CheckinListItemCheckinViewModel Checkin { get; set; }

        public CheckinListItemRatingViewModel Rating { get; set; }

        public CheckinListItemCommentViewModel Comment { get; set; }
    }
}