using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands;
using Lab.RateMyBeer.Comments.Contracts.Comments.ApiClient;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;
using Lab.RateMyBeer.Framework.Composition.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.Commands;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinDetails;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinList;
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
        private readonly IViewModelCompositionContext _compositionContext;

        public CheckinsController(IMessageSession messageSession, ICheckinsRestApi checkinsRestApi, ICommentsRestApi commentsRestApi, IRatingsRestApi ratingsRestApi, IViewModelCompositionContext compositionContext)
        {
            _messageSession = messageSession;
            _checkinsRestApi = checkinsRestApi;
            _commentsRestApi = commentsRestApi;
            _ratingsRestApi = ratingsRestApi;
            _compositionContext = compositionContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = new CheckinListViewModel();

            _compositionContext.SetValue("page", 1);
            result = await  _compositionContext.Compose<CheckinListViewModel>(result);

            return Ok(result);
        }

        private async Task<CheckinListViewModel> ComposeCheckinListDirect()
        {
            var checkins = await _checkinsRestApi.GetAll();
            var checkinDtos = checkins.Items.ToList();
            var checkinIds = checkinDtos.Select(c => c.CheckinId).ToList();

            var ratingsTask = _ratingsRestApi.GetByCheckinIds(checkinIds);
            var commentsTask = _commentsRestApi.GetByCheckinIds(checkinIds);

            var comments = await commentsTask;
            var ratings = await ratingsTask;

            return new CheckinListViewModel()
            {
                Items = checkinDtos.Select(dto => new CheckinListItemViewModel()
                {
                    CheckinId = dto.CheckinId,

                    Checkin = new CheckinListItemCheckinViewModel()
                    {
                        BeerName = dto.BeerName,
                        UserId = dto.UserId,
                        CreatedAt = dto.CreatedAt,
                    },

                    Rating = new CheckinListItemRatingViewModel()
                    {
                        RatingCategory = ratings.Items.SingleOrDefault(r => r.CheckinId == dto.CheckinId)?.Description ??
                                         string.Empty,
                    },

                    Comment = new CheckinListItemCommentViewModel()
                    {
                        UserComment = comments.Items.SingleOrDefault(c => c.CheckinId == dto.CheckinId)?.UserComment ??
                                      string.Empty
                    }
                }).ToList()
            };
        }
        
        [HttpGet("{checkinId}")]
        public async Task<IActionResult> GetCheckinById([FromRoute] Guid checkinId)
        {
            var result = new CheckinDetailsViewModel()
            {
                CheckinId = checkinId
            };

            result = await _compositionContext.Compose<CheckinDetailsViewModel>(result);
            
            return Ok(result);
        }

        private async Task<CheckinDetailsViewModel> ComposeCheckinDetailsDirect(Guid checkinId)
        {
            var checkinsTask = _checkinsRestApi.GetByIds(new[] { checkinId });
            var ratingsTask = _ratingsRestApi.GetByCheckinIds(new[] { checkinId });
            var commentsTask = _commentsRestApi.GetByCheckinIds(new[] { checkinId });

            var checkin = (await checkinsTask).Items.Single();
            var comment = (await commentsTask).Items.Single();
            var rating = (await ratingsTask).Items.Single();

            return new CheckinDetailsViewModel()
            {
                CheckinId = checkin.CheckinId,

                Checkin = new CheckinDetailsCheckinViewModel()
                {
                    BeerName = checkin.BeerName,
                    UserId = checkin.UserId,
                    CheckinId = checkin.CheckinId,
                    CreatedAt = checkin.CreatedAt
                },

                Rating = new CheckinDetailsRatingViewModel()
                {
                    RatingCategory = rating.Description,
                    StarRating = rating.Rating,
                },

                Comments = new CheckinDetailsCommentsViewModel()
                {
                    UserComment = comment.UserComment,
                    BreweryComment = comment.BreweryComment,
                    Comments = new CheckinDetailsCommentListViewModel()
                    {
                        Items = comment.Comments.Select(c => new CheckinDetailsCommentListItemViewModel()
                        {
                            UserId = c.UserId,
                            Comment = c.Comment
                        }).ToList()
                    }
                }
            };
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