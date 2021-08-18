namespace DiamondBet.Web.ViewModels.Bets
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DiamondBet.Data.Models.Enums;

    public class BetsInListViewModel
    {
        public string Id { get; set; }

        public int GameId { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public byte? HomeTeamGoals { get; set; }

        public byte? AwayTeamGoals { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public decimal BetOdds { get; set; }

        public string Prediction { get; set; }

        public string BetStatus { get; set; }
    }
}
