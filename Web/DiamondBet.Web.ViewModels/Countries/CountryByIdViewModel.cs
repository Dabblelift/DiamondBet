namespace DiamondBet.Web.ViewModels.Countries
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DiamondBet.Web.ViewModels.Teams;

    public class CountryByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Capital { get; set; }

        public IEnumerable<TeamsInCountryViewModel> Teams { get; set; }
    }
}
