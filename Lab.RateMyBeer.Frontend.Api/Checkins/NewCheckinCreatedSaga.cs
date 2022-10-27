using System;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Events;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Events;
using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Events;
using NServiceBus;

namespace Lab.RateMyBeer.Frontend.Api.Checkins;

public class NewCheckinCreatedSaga : Saga<NewCheckinCreatedSagaData>
    , IAmStartedByMessages<CreateCheckinSucceededEvent>, IAmStartedByMessages<CreateCheckinFailedEvent>
    , IAmStartedByMessages<CreateCommentSucceededEvent>, IAmStartedByMessages<CreateCommentFailedEvent>
    , IAmStartedByMessages<CreateStarRatingSucceededEvent>, IAmStartedByMessages<CreateStarRatingFailedEvent>
{
    public async Task Handle(CreateCheckinSucceededEvent message, IMessageHandlerContext context)
    {
        Data.CheckinId = message.CheckinId;
        Data.CreateCheckinSucceeded = true;
        await NotifyUi(context);
    }

    public async Task Handle(CreateCheckinFailedEvent message, IMessageHandlerContext context)
    {
        Data.CheckinId = message.CheckinId;
        Data.CreateCheckinSucceeded = false;
        await NotifyUi(context);
    }

    public async Task Handle(CreateCommentSucceededEvent message, IMessageHandlerContext context)
    {
        Data.CheckinId = message.CheckinId;
        Data.CreateCommentSucceeded = true;
        await NotifyUi(context);
    }

    public async Task Handle(CreateCommentFailedEvent message, IMessageHandlerContext context)
    {
        Data.CheckinId = message.CheckinId;
        Data.CreateCommentSucceeded = false;
        await NotifyUi(context);
    }

    public async Task Handle(CreateStarRatingSucceededEvent message, IMessageHandlerContext context)
    {
        Data.CheckinId = message.CheckinId;
        Data.CreateStarRatingSucceeded = true;
        await NotifyUi(context);
    }

    public async Task Handle(CreateStarRatingFailedEvent message, IMessageHandlerContext context)
    {
        Data.CheckinId = message.CheckinId;
        Data.CreateStarRatingSucceeded = false;
        await NotifyUi(context);
    }

    public async Task NotifyUi(IMessageHandlerContext context)
    {
        if (Data.ClientHasBeenNotfied)
            return;

        if (Data.CompletedSuccessfully)
        {
            // Use SignalR to notify the UI   
            Data.ClientHasBeenNotfied = true;
        }

        if (Data.CompletedWitFailure)
        {
            // Use SignalR to notify the UI   
            Data.ClientHasBeenNotfied = true;
        }

        await Task.CompletedTask;
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<NewCheckinCreatedSagaData> mapper)
    {
        mapper.MapSaga(d => d.CheckinId)
            .ToMessage<CreateCheckinSucceededEvent>(m => m.CheckinId)
            .ToMessage<CreateCheckinFailedEvent>(m => m.CheckinId)
            .ToMessage<CreateCommentSucceededEvent>(m => m.CheckinId)
            .ToMessage<CreateCommentFailedEvent>(m => m.CheckinId)
            .ToMessage<CreateStarRatingSucceededEvent>(m => m.CheckinId)
            .ToMessage<CreateStarRatingFailedEvent>(m => m.CheckinId)
            ;
    }
}

public class NewCheckinCreatedSagaData : ContainSagaData
{

    public Guid CheckinId { get; set; }

    public bool? CreateCheckinSucceeded { get; set; }
    public bool? CreateStarRatingSucceeded { get; set; }
    public bool? CreateCommentSucceeded { get; set; }

    public bool ClientHasBeenNotfied {  get; set; }

    public bool Completed => CreateCheckinSucceeded.HasValue && CreateStarRatingSucceeded.HasValue && CreateCommentSucceeded.HasValue;
    public bool CompletedSuccessfully => Completed && CreateCheckinSucceeded.Value && CreateCommentSucceeded.Value && CreateStarRatingSucceeded.Value;
    public bool CompletedWitFailure => Completed && !CompletedSuccessfully;

}
