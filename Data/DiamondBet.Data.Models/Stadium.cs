namespace DiamondBet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using DiamondBet.Data.Common.Models;

    public class Stadium : BaseDeletableModel<int>
    {
        public Stadium()
        {
            this.Games = new HashSet<Game>();
        }

        [Required]
        [StringLength(70, MinimumLength = 3)]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [Required]
        [Range(0, 200000)]
        public int Capacity { get; set; }

        [Required]
        public int YearFounded { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
