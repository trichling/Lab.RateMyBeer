using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Commands;
using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Events;
using Lab.RateMyBeer.Ratings.Data.StarRatings;
using NServiceBus;

namespace Lab.RateMyBeer.Ratings.StarRatings
{
    public class CreateStarRatingHandler : IHandleMessages<CreateStarRatingCommand>
    {
        private readonly StarRatingContext _context;

        public CreateStarRatingHandler(StarRatingContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateStarRatingCommand message, IMessageHandlerContext context)
        {
            var description = string.Empty;
            if (message.Rating < 2)
            {
                description = "Mies";
            }

            if (message.Rating == 2 || message.Rating == 3)
            {
                description = "Ok";
            }

            if (message.Rating >= 4)
            {
                description = "Super";
            }

            var rating = new StarRatingData(message.RatingId, message.CheckinId, message.Rating, description);
          
            _context.StarRatings.Add(rating);
            await _context.SaveChangesAsync();

            await context.Publish(new CreateStarRatingSucceededEvent(message.CheckinId));
        }
    }
}