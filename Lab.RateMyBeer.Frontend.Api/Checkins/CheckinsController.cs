using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.Commands;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;
using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Lab.RateMyBeer.Frontend.Api.Checkins
{
    [ApiController]
    [Route("checkins")]
    public class CheckinsController : ControllerBase
    {

        private readonly IMessageSession _messageSession;
        private readonly ICheckinsRestApi _checkinsRestApi;

        public CheckinsController(IMessageSession messageSession, ICheckinsRestApi checkinsRestApi)
        {
            _messageSession = messageSession;
            _checkinsRestApi = checkinsRestApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var checkinsFromApi = await _checkinsRestApi.GetAll();

            return Ok(new CheckinListViewModel
            {
                Items = checkinsFromApi.Select(dto => new CheckinListItemViewModel()
                {
                    BeerName = dto.BeerName,
                    UserId = dto.UserId,
                    CheckinId = dto.CheckinId,
                    CreatedAt = dto.CreatedAt
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckin(CreateCheckinCommandViewModel createCheckinCommand)
        {
            var checkinId = Guid.NewGuid();
            var userId = GetUserIdFromBearerToken();
                
            await _messageSession.Send(new CreateCheckinCommand()
            {
                CheckinId = checkinId,
                UserId = userId,
                CreatedAt = DateTime.Now,
                BeerName = createCheckinCommand.BeerName

            });
            
            await _messageSession.Send(new CreateStarRatingCommand(
                RatingId: Guid.NewGuid(),
                CheckinId: checkinId,
                UserId: userId,
                Rating: createCheckinCommand.StarRating
            ));

            await _messageSession.Send(new CreateCommentCommand(
                CommentId: Guid.NewGuid(),
                CheckinId: checkinId,
                UserId: userId,
                Comment: createCheckinCommand.Comment
            ));

            return Ok(new { checkinId });
        }

        private Guid GetUserIdFromBearerToken()
        {
            return new Guid("FBBB72AF-7D6A-4507-ADAF-18EB61964633");
        }

    }
}