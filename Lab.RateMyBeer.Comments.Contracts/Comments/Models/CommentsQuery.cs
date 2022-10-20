using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Lab.RateMyBeer.Comments.Contracts.Comments.Models;

public class CheckinIds : List<Guid>
{
    private CheckinIds(IEnumerable<Guid> ids) : base(ids)
    {
    }

    public static bool TryParse(string value, out CheckinIds result)
    {
        result = new CheckinIds(value.Split(",").Select(v => Guid.Parse(v)));
        return true;
    }
    
    public static ValueTask<CheckinIds?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        const string key = "checkinIds";

        var ids = context.Request.Query[key].Select(s => Guid.Parse(s));

        var result = new CheckinIds(ids);
     

        return ValueTask.FromResult<CheckinIds?>(result);
    }
}