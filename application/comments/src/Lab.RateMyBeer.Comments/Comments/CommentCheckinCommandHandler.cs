﻿using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Events;
using Lab.RateMyBeer.Comments.Data.Comments;
using NServiceBus;

namespace Lab.RateMyBeer.Comments.Comments;

public class CommentCheckinCommandHandler : IHandleMessages<CommentCheckinCommand>
{
    private readonly CommentsContext _context;

    public CommentCheckinCommandHandler(CommentsContext context)
    {
        _context = context;
    }
    
    public async Task Handle(CommentCheckinCommand message, IMessageHandlerContext context)
    {
        var comments = _context.Comments.SingleOrDefault(cs => cs.CheckinId == message.CheckinId);
        
        if (comments == null)
        {
            comments = new CommentsData(
                commentsId: Guid.NewGuid(),
                checkinId: message.CheckinId,
                comments: new List<CommentData>()
            );

            _context.Comments.Add(comments);
        }

        comments.Comments.Add(new CommentData(
            commentId: Guid.Empty,
            userId: message.UserId,
            comment: message.Comment
        ));

        await _context.SaveChangesAsync();
        
        await context.Publish(new CreateCommentSucceededEvent()
        {
            CheckinId = message.CheckinId,
            CommentId = message.CommentId
        });
    }
}