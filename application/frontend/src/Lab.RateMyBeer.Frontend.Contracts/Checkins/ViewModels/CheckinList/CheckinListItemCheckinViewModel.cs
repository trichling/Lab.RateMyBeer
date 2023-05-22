using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinList;

public class CheckinListItemCheckinViewModel
{
        
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string BeerName { get; set; }
        
}