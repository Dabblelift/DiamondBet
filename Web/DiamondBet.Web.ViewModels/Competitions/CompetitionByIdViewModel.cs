namespace DiamondBet.Web.ViewModels.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DiamondBet.Web.ViewModels.Games;

    public class CompetitionByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte NumberOfParticipants { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<FilteredGamesViewModel> PreviousGames { get; set; }

        public IEnumerable<FilteredGamesViewModel> UpcomingGames { get; set; }
    }
}
