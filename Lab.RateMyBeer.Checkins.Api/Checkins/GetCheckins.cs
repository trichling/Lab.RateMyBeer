using Lab.RateMyBeer.Checkins.Contracts.Checkins.Models;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Checkins.Api.Checkins;

public static class GetCheckins
{
    public static async Task<IResult> Handle(CheckinsContext context)
    {
        var checkins = await context.Checkins.ToListAsync();
        
        var checkinDtos = checkins.Select(c => new CheckinDto()
        {
            BeerName = c.BeerName,
            CheckinId = c.CheckinId,
            CreatedAt = c.CreatedAt,
            UserId = c.UserId
        }).ToList();

        return Results.Ok(new AllCheckinsDto(checkinDtos));
    }
}