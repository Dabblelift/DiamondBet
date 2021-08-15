namespace DiamondBet.Web.ViewModels.Bets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using DiamondBet.Data.Models.Enums;

    public class EditBetInputModel
    {
        [Required]
        public int GameId { get; set; }

        public string Prediction { get; set; }

        public string BetStatus { get; set; }

        public decimal Stake { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
