using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands
{
    public class CreateCheckinCommand
    {
        public Guid CheckinId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BeerName { get; set; }
    }
}
