using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands;
using Lab.RateMyBeer.Comments.Contracts.Comments.ApiClient;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.Commands;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;
using Lab.RateMyBeer.Ratings.Contracts.StarRatings;
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
        private readonly ICommentsRestApi _commentsRestApi;
        private readonly IRatingsRestApi _ratingsRestApi;

        public CheckinsController(IMessageSession messageSession, ICheckinsRestApi checkinsRestApi, ICommentsRestApi commentsRestApi, IRatingsRestApi ratingsRestApi)
        {
            _messageSession = messageSession;
            _checkinsRestApi = checkinsRestApi;
            _commentsRestApi = commentsRestApi;
            _ratingsRestApi = ratingsRestApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var checkins = await _checkinsRestApi.GetAll();
            var checkinDtos = checkins.Items.ToList();
            var checkinIds = checkinDtos.Select(c => c.CheckinId).ToList();

            var ratingsTask = _ratingsRestApi.GetByCheckinIds(checkinIds);
            var commentsTask = _commentsRestApi.GetByCheckinIds(checkinIds);
            // not needed this way
            // Task.WaitAll(ratingsTask, commentsTask);
            var comments = await commentsTask;
            var ratings = await ratingsTask;
            
            return Ok(new CheckinListViewModel
            {
                Items = checkinDtos.Select(dto => new CheckinListItemViewModel()
                {
                    BeerName = dto.BeerName,
                    UserId = dto.UserId,
                    CheckinId = dto.CheckinId,
                    CreatedAt = dto.CreatedAt,
                    RatingCategory = ratings.Items.SingleOrDefault(r => r.CheckinId == dto.CheckinId)?.Description ?? string.Empty,
                    UserComment = comments.Items.SingleOrDefault(c => c.CheckinId == dto.CheckinId)?.UserComment ?? string.Empty
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

            await _messageSession.Send(new CreateUserCommentCommand(
                CommentId: Guid.NewGuid(),
                CheckinId: checkinId,
                UserId: userId,
                UserComment: createCheckinCommand.UserComment
            ));

            return Ok(new { checkinId });
        }

        [HttpPost("comments")]
        public async Task<IActionResult> CommentCheckin(CommentCheckinCommandViewModel commentCheckinCommand)
        {
            var userId = GetUserIdFromBearerToken();

            await _messageSession.Send(new CommentCheckinCommand(
                CheckinId: commentCheckinCommand.CheckinId,
                CommentId: Guid.NewGuid(),
                UserId: userId,
                Comment: commentCheckinCommand.Comment
            ));
            
            return Ok();
        }
        
        private Guid GetUserIdFromBearerToken()
        {
            return new Guid("FBBB72AF-7D6A-4507-ADAF-18EB61964633");
        }

    }
}