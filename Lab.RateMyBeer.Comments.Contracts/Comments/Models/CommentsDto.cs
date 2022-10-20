namespace Lab.RateMyBeer.Comments.Contracts.Comments.Models;

public record CommentsDto(Guid Id, Guid CheckinId, List<CommentDto> Comments);
public record CommentDto(Guid Id, Guid CheckinId, Guid UserId, string Comment);