namespace DiamondBet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using DiamondBet.Data.Common.Models;
    using DiamondBet.Data.Models.Enums;

    public class Bet : BaseDeletableModel<string>
    {
        private decimal betOdds;
        private decimal stake;

        public Bet()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public BetType BetType { get; set; }

        public Prediction Prediction { get; set; }

        public BetStatus BetStatus { get; set; }

        [Required]
        public decimal Stake
        {
            get
            {
                return this.stake;
            }

            set
            {
                this.stake = value;
                this.ReturnOnWin = this.Stake * this.BetOdds;
            }
        }

        [Required]
        public decimal BetOdds
        {
            get
            {
                return this.betOdds;
            }

            set
            {
                this.betOdds = value;
                this.ReturnOnWin = this.Stake * this.BetOdds;
            }
        }

        public decimal ReturnOnWin { get; private set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
