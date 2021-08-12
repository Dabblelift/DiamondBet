namespace DiamondBet.Web.ViewModels.Stadiums
{
    using DiamondBet.Web.ViewModels.CustomValidation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddStadiumInputModel
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 200000)]
        public int Capacity { get; set; }

        [Required]
        [CurrentYearMaxValue(1850)]
        [Display(Name = "Year Founded")]
        public int YearFounded { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }
    }
}
