namespace DiamondBet.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class UserRankingViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UserRankingViewComponent(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IViewComponentResult Invoke()
        {
            var users = this.usersRepository
                .AllAsNoTracking()
                .Where(x => x.UserBets.Count > 0)
                .Select(x => new UsersInListViewModel
                {
                    UserId = x.Id,
                    Username = x.UserName,
                    Coins = x.Coins,
                    BetsCount = x.UserBets.Count,
                })
                .OrderByDescending(x => x.Coins)
                .ThenBy(x => x.BetsCount).ToList();

            var model = new UserRankingViewModel { Users = users };

            return this.View(model);
        }
    }
}
