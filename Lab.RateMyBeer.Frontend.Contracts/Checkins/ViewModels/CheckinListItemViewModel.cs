using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels
{
    public class CheckinListItemViewModel
    {
        public Guid CheckinId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BeerName { get; set; }
    }
}