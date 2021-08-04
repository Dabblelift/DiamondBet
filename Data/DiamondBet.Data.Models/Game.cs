namespace DiamondBet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using DiamondBet.Data.Common.Models;

    public class Game : BaseDeletableModel<int>
    {
        public Game()
        {
            this.Bets = new HashSet<Bet>();
        }

        public DateTime StartingTime { get; set; }

        [ForeignKey(nameof(HomeTeam))]
        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        [ForeignKey(nameof(AwayTeam))]
        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        public byte? HomeGoals { get; set; }

        public byte? AwayGoals { get; set; }

        [ForeignKey(nameof(Odds))]
        public int OddsId { get; set; }

        public Odds Odds { get; set; }

        [ForeignKey(nameof(Competition))]
        public int ComnetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public int StadiumId { get; set; }

        public virtual Stadium Stadium { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
