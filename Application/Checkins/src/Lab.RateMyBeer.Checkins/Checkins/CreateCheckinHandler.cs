using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Events;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using NServiceBus;

namespace Lab.RateMyBeer.Checkins.Checkins
{
    public class CreateCheckinHandler : IHandleMessages<CreateCheckinCommand>
    {
        private readonly CheckinsContext _context;

        public CreateCheckinHandler(CheckinsContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCheckinCommand message, IMessageHandlerContext context)
        {

            await _context.Checkins.AddAsync(new CheckinData()
            {
                CheckinId = message.CheckinId,
                UserId = message.UserId,
                CreatedAt = message.CreatedAt,
                BeerName = message.BeerName
            });

            await _context.SaveChangesAsync();

            await context.Publish(new CreateCheckinSucceededEvent()
            {
                CheckinId = message.CheckinId
            });

        }
    }
}
