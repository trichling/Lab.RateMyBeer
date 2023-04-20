using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Events
{
    public class CreateCheckinSucceededEvent
    {

        public Guid CheckinId { get; set; }


    }
}
