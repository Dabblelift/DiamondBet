namespace DiamondBet.Web.ViewModels.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddCompetitionInputModel
    {
        [Required]
        [StringLength(70, MinimumLength =5)]
        public string Name { get; set; }

        [Display(Name = "Number of participants")]
        [Range(2, 200)]
        public byte NumberOfParticipants { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }
    }
}
