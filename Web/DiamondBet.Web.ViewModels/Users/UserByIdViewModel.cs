namespace DiamondBet.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DiamondBet.Web.ViewModels.Bets;

    public class UserByIdViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public decimal Coins { get; set; }

        public int FirstPlaces { get; set; }

        public int SecondPlaces { get; set; }

        public int ThirdPlaces { get; set; }

        public string RegisteredOn { get; set; }

        public IEnumerable<BetsInListViewModel> Bets { get; set; }
    }
}
