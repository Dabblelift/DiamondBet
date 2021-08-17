namespace DiamondBet.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserCoinsViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserCoinsViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke(ClaimsPrincipal userClaim)
        {
            var user = this.userManager.GetUserAsync(userClaim);

            var coins = user.Result.Coins;

            var model = new UserCoinsViewModel
            {
                Coins = coins,
            };

            return this.View(model);
        }
    }
}
