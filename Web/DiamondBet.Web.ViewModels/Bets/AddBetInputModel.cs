namespace DiamondBet.Web.ViewModels.Bets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using DiamondBet.Data.Models.Enums;

    public class AddBetInputModel
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public string Prediction { get; set; }

        [Range(1, 1000)]
        public decimal Stake { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
