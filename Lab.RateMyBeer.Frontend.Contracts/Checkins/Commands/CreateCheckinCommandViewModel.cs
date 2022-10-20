using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.Commands
{
    public class CreateCheckinCommandViewModel
    {
        public string BeerName { get; set; }
        
        public int StarRating { get; set; }
        
        public string Comment { get; set; }

    }
}