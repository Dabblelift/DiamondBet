namespace DiamondBet.Web.ViewModels.Teams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using DiamondBet.Web.ViewModels.CustomValidation;

    public class EditTeamInputModel
    {
        [Required]
        [StringLength(70, MinimumLength = 2)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [CurrentYearMaxValue(1800)]
        [Display(Name = "Year Founded")]
        public int YearFounded { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }
    }
}
