namespace DiamondBet.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GamеsInListViewModel
    {
        public int Id { get; set; }

        public string StartingTime { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public OddsViewModel Odds { get; set; }

        public byte? HomeTeamGoals { get; set; }

        public byte? AwayTeamGoals { get; set; }
    }
}
