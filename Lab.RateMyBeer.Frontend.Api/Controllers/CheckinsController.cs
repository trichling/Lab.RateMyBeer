using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.Commands;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace Lab.RateMyBeer.Frontend.Api.Controllers
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

            return Ok(new CheckinListViewModel { Items = checkinsFromApi.Select(dto => new CheckinListItemViewModel()
            {
                BeerName = dto.BeerName,
                UserId = dto.UserId,
                CheckinId = dto.CheckinId,
                CreatedAt = dto.CreatedAt
            }).ToList() } );
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckin(CreateCheckinCommandViewModel createCheckinCommand)
        {
           
            await _messageSession.Send(new CreateCheckinCommand()
            {
                CheckinId = Guid.NewGuid(),
                UserId = GetUserId(),
                CreatedAt = DateTime.Now,
                BeerName = createCheckinCommand.BeerName

            });

            return Ok();
        }

        private Guid GetUserId()
        {
            return new Guid("FBBB72AF-7D6A-4507-ADAF-18EB61964633");
        }

    }
}