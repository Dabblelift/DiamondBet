namespace DiamondBet.Web.ViewModels.Teams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddTeamInputModel
    {
        [Required]
        [StringLength(70, MinimumLength = 2)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Nickname { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }

    }
}
