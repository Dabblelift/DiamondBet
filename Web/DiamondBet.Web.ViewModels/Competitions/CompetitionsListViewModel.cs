namespace DiamondBet.Web.ViewModels.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompetitionsListViewModel
    {
        public IEnumerable<CompetitionsInListViewModel> Competitions { get; set; }
    }
}
