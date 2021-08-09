namespace DiamondBet.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GamesListViewModel
    {
        public IEnumerable<GamеsInListViewModel> Games { get; set; }
    }
}
