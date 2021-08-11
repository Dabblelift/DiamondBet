namespace DiamondBet.Web.ViewModels.Teams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DiamondBet.Web.ViewModels.Games;

    public class TeamByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public int YearFounded { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<TeamGamesViewModel> PreviousGames { get; set; }

        public IEnumerable<TeamGamesViewModel> UpcomingGames { get; set; }
    }
}
