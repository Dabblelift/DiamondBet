namespace DiamondBet.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TeamGamesViewModel
    {
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public byte? HomeGoals { get; set; }

        public byte? AwayGoals { get; set; }

        public DateTime StartingTime { get; set; }
    }
}
