namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels;

public class CheckinDetailsCommentsViewModel
{
    public string UserComment { get; set; }
    public string BreweryComment { get; set; }

    public CheckinDetailsCommentListViewModel Comments { get; set; }
}