namespace DiamondBet.Web.ViewModels.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompetitionsInListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public byte NumberOfParticipants { get; set; }
    }
}
