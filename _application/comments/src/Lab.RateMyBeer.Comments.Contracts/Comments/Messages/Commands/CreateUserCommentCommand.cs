namespace Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands
{
    public record CreateUserCommentCommand(Guid CommentId, Guid CheckinId, Guid UserId, string UserComment);
}