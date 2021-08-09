namespace DiamondBet.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameByIdViewModel
    {
        public int Id { get; set; }

        public DateTime StartingTime { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public byte? HomeGoals { get; set; }

        public byte? AwayGoals { get; set; }

        public OddsViewModel Odds { get; set; }

        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public int StadiumId { get; set; }

        public string StadiumName { get; set; }
    }
}
