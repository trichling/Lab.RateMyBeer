namespace Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;

public record CommentCheckinCommand(Guid CommentId, Guid CheckinId, Guid UserId, string Comment);
