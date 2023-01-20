using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels
{
    public class CheckinListItemViewModel
    {
        public Guid CheckinId { get; set; }
        
        public CheckinListItemCheckinViewModel Checkin { get; set; }

        public CheckinListItemRatingViewModel Rating { get; set; }

        public CheckinListItemCommentViewModel Comment { get; set; }
    }

    public class CheckinListItemCommentViewModel
    {
        public string UserComment { get; set; }
    }

    public class CheckinListItemRatingViewModel
    {
        public string RatingCategory { get; set; }
    }

    public class CheckinListItemCheckinViewModel
    {
        
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BeerName { get; set; }
        
    }
}