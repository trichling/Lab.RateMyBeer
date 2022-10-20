namespace Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands
{
    public record CreateCommentCommand(Guid CommentId, Guid CheckinId, Guid UserId, string Comment);
}