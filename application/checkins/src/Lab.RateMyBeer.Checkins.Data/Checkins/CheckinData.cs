using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.RateMyBeer.Checkins.Data.Checkins
{
    public class CheckinData
    {

        public Guid CheckinId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BeerName { get; set; }

    }
}
