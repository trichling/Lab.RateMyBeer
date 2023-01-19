using System;
using System.Collections.Generic;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;

public class CheckinDetailsViewModel
{
    public Guid CheckinId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string BeerName { get; set; }

    public int StarRating { get; set; }
    public string RatingCategory { get; set; }
        
    public string UserComment { get; set; }
    public string BreweryComment { get; set; }

    public List<CheckinDetailsCommentViewModel> Comments { get; set; }

}

public class CheckinDetailsCommentViewModel
{

    public Guid UserId { get; set; }
    public string Comment { get; set; }
    
}