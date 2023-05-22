using Lab.RateMyBeer.Checkins.Contracts.Checkins.Models;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Checkins.Api.Checkins;

public class GetCheckinsByIds
{
    public static async Task<IResult> Handle([FromQuery] CheckinIds checkinIds, [FromServices] CheckinsContext context)
    {
        var checkins = await context.Checkins.Where(c => checkinIds.Contains(c.CheckinId)).ToListAsync();
        
        var checkinDtos = checkins.Select(c => new CheckinDto()
        {
            BeerName = c.BeerName,
            CheckinId = c.CheckinId,
            CreatedAt = c.CreatedAt,
            UserId = c.UserId
        }).ToList();

        return Results.Ok(new CheckinsDto(checkinDtos));
    }
}