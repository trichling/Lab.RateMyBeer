using Lab.RateMyBeer.Comments.Contracts.Comments.Models;
using Lab.RateMyBeer.Comments.Data.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Comments.Api.Comments;

public static class GetComments
{
    public static async Task<IResult> Handle([FromQuery] CheckinIds checkinIds, [FromServices] CommentsContext context)
    {
        var comments = await context.Comments.Where(cs => checkinIds.Contains(cs.CheckinId))
                        .Include(c => c.Comments)
                        .ToListAsync();

        var commentsDtos = comments.Select(cs => new CommentsDto(
            Id: cs.CommentsId,
            CheckinId:cs.CheckinId,
            UserComment:cs.UserComment,
            BreweryComment:cs.BreweryComment,
            Comments: cs.Comments.Select(c => new CommentDto(
                Id: c.CommentId,
                CheckinId:cs.CheckinId,
                UserId: c.UserId,
                Comment: c.Comment
            )).ToList()
        )).ToList();

        return Results.Ok(new CommentsByCheckinIdsDto(commentsDtos));
    }
}