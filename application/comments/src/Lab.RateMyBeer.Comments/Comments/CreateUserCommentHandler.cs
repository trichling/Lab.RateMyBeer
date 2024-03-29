using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Events;
using Lab.RateMyBeer.Comments.Data.Comments;
using NServiceBus;

namespace Lab.RateMyBeer.Comments.Comments;

public class CreateUserCommentHandler : IHandleMessages<CreateUserCommentCommand>
{
    private readonly CommentsContext _context;

    public CreateUserCommentHandler(CommentsContext context)
    {
        _context = context;
    }
    
    public async Task Handle(CreateUserCommentCommand message, IMessageHandlerContext context)
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

        comments.UserComment = message.UserComment;
        
        // comments.Comments.Add(new CommentData(
        //     commentId: message.CommentId,
        //     userId: message.UserId,
        //     comment: message.Comment
        // ));

        await _context.SaveChangesAsync();
        
        await context.Publish(new CreateCommentSucceededEvent()
        {
            CheckinId = message.CheckinId,
            CommentId = message.CommentId
        });
    }
}