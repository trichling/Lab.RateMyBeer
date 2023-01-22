using System;
using System.Collections.Generic;
using Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels.CheckinList;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.ViewModels
{
    public class CheckinListViewModel
    {
        public List<CheckinListItemViewModel> Items { get; set; }

    }
}