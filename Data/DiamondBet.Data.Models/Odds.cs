namespace DiamondBet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using DiamondBet.Data.Common.Models;

    public class Odds : BaseDeletableModel<int>
    {
        [ForeignKey(nameof(Game))]
        public int? GameId { get; set; }

        public virtual Game Game { get; set; }

        public decimal HomeWinOdds { get; set; }

        public decimal DrawOdds { get; set; }

        public decimal AwayWinOdds { get; set; }

        public decimal OverOdds { get; set; }

        public decimal UnderOdds { get; set; }

        public decimal BTTSYesOdds { get; set; }

        public decimal BTTSNoOdds { get; set; }
    }
}
