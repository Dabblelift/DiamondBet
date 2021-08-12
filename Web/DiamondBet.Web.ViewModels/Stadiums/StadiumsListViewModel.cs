namespace DiamondBet.Web.ViewModels.Stadiums
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StadiumsListViewModel
    {
        public IEnumerable<StadiumsInListViewModel> Stadiums { get; set; }
    }
}
