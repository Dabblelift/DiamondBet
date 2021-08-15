namespace DiamondBet.Web.ViewModels.Bets
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BetsListViewModel
    {
        public IEnumerable<BetsInListViewModel> Bets { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }
    }
}
