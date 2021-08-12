namespace DiamondBet.Web.ViewModels.Countries
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CountriesListViewModel
    {
        public IEnumerable<CountriesInListViewModel> Countries { get; set; }
    }
}
