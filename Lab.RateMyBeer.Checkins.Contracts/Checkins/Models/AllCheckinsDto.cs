using System.Collections.Generic;

namespace Lab.RateMyBeer.Checkins.Contracts.Checkins.Models
{
    public record AllCheckinsDto(List<CheckinDto> Items);
}