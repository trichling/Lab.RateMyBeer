using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.Commands
{
    public record CreateCheckinCommandViewModel(string BeerName, int StarRating, string UserComment);
    
}