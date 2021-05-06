using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Models;
using RestEase;

namespace Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient
{
    public interface ICheckinsRestApi
    {

        [Get("checkins")]
        public Task<IEnumerable<CheckinDto>> GetAll();

    }
}
