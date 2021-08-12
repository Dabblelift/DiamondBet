namespace DiamondBet.Web.ViewModels.Stadiums
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DiamondBet.Web.ViewModels.Games;

    public class StadiumByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int YearFounded { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<FilteredGamesViewModel> Games { get; set; }
    }
}
