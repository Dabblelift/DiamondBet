namespace DiamondBet.Web.ViewModels.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UsersInListViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public decimal Coins { get; set; }

        public int BetsCount { get; set; }
    }
}
