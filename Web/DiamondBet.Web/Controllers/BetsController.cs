namespace DiamondBet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DiamondBet.Data.Models;
    using DiamondBet.Data.Models.Enums;
    using DiamondBet.Services.Data;
    using DiamondBet.Web.ViewModels.Bets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BetsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBetsService betsService;

        public BetsController(
            UserManager<ApplicationUser> userManager,
            IBetsService betsService)
        {
            this.userManager = userManager;
            this.betsService = betsService;
        }

        [Authorize]
        public IActionResult ByUser(string id)
        {
            var bets = this.betsService.GetBetsByUserId(id);
            var model = new BetsListViewModel
            {
                Bets = bets,
                UserId = this.userManager.GetUserId(this.User),
            };

            return this.View(model);
        }

        [Authorize]
        public IActionResult All()
        {
            var bets = this.betsService.GetAllBets();
            var model = new BetsListViewModel
            {
                Bets = bets,
                UserId = this.userManager.GetUserId(this.User),
            };
            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create(int id, decimal stake, int predictionId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user == null)
            {
                return this.RedirectToAction("Error", "Bets");
            }

            var model = new AddBetInputModel { Stake = stake, Prediction = Enum.GetName(typeof(Prediction), predictionId), GameId = id, UserId = user.Id };

            if (this.betsService.CheckIfValid(model, user))
            {
                await this.betsService.AddBetAsync(model);
                this.TempData["Message"] = "Successfully made a bet.";
                return this.RedirectToAction(nameof(this.ByUser), new { id = user.Id });
            }
            else
            {
                return this.RedirectToAction("Error", "Bets");
            }
        }

        [Authorize]
        public IActionResult ById(string id)
        {
            var model = this.betsService.GetById(id);
            return this.View(model);
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
