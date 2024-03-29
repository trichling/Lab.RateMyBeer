using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinDetails;

public class CheckinDetailsCheckinViewModel
{
    public Guid CheckinId { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string BeerName { get; set; }
}