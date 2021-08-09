namespace DiamondBet.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using DiamondBet.Web.ViewModels.CustomValidation;

    public class EditGameInputModel
    {
        [Display(Name = "Home Team")]
        public int HomeTeamId { get; set; }

        [Display(Name = "Away Team")]
        [NotEqualTo(nameof(HomeTeamId), ErrorMessage = "Home Team and Away Team cannot have the same values")]
        public int AwayTeamId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TeamsItems { get; set; }

        [Display(Name = "Starting Time")]
        public DateTime StartingTime { get; set; }

        [Display(Name = "Home Goals")]
        public byte? HomeGoals { get; set; }

        [Display(Name = "Away Goals")]
        public byte? AwayGoals { get; set; }

        public OddsInputModel Odds { get; set; }

        [Display(Name = "Competition")]
        public int CompetitionId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CompetitionsItems { get; set; }

        [Display(Name = "Stadium")]
        public int StadiumId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> StadiumsItems { get; set; }
    }
}
