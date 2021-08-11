namespace DiamondBet.Web.ViewModels.Teams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TeamsListViewModel
    {
        public IEnumerable<TeamsInListViewModel> Teams { get; set; }
    }
}
