namespace DiamondBet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using DiamondBet.Data.Common.Models;

    public class Country : BaseModel<int>
    {
        public Country()
        {
            this.Teams = new HashSet<Team>();
            this.Stadiums = new HashSet<Stadium>();
            this.Competitions = new HashSet<Competition>();
        }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Stadium> Stadiums { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
