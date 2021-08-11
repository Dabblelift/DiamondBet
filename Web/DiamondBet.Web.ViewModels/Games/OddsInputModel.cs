namespace DiamondBet.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class OddsInputModel
    {
        [Display(Name = "Home Win")]
        public decimal HomeWinOdds { get; set; }

        [Display(Name = "Away Win")]
        public decimal AwayWinOdds { get; set; }

        [Display(Name = "Draw")]
        public decimal DrawOdds { get; set; }

        [Display(Name = "Over 2.5")]
        public decimal OverOdds { get; set; }

        [Display(Name = "Under 2.5")]
        public decimal UnderOdds { get; set; }

        [Display(Name = "BTTS - Yes")]
        public decimal BTTSYesOdds { get; set; }

        [Display(Name = "BTTS - No")]
        public decimal BTTSNoOdds { get; set; }
    }
}
