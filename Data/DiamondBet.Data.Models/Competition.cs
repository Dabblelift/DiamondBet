namespace DiamondBet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using DiamondBet.Data.Common.Models;

    public class Competition : BaseDeletableModel<int>
    {
        public Competition()
        {
            this.Games = new HashSet<Game>();
        }

        [Required]
        [StringLength(70, MinimumLength = 5)]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
