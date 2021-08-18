namespace DiamondBet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DiamondBet.Services.Data;
    using DiamondBet.Web.ViewModels.Games;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : BaseController
    {
        private readonly IGamesService gamesService;
        private readonly IBetsService betsService;
        private readonly ISelectItemsService selectItemsService;

        public GamesController(
            IGamesService gamesService,
            IBetsService betsService,
            ISelectItemsService selectItemsService)
        {
            this.gamesService = gamesService;
            this.betsService = betsService;
            this.selectItemsService = selectItemsService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.gamesService.DeleteGameAsync(id);
            return this.RedirectToAction(nameof(this.UpcomingGames));
        }

        public IActionResult UpcomingGames(int? id)
        {
            if (id != null)
            {
                var teamGames = this.gamesService.GetUpcomingGamesByTeam((int)id);
                var parameterModel = new GamesListViewModel { Games = teamGames };
                return this.View(parameterModel);
            }

            var games = this.gamesService.GetAllUpcomingGames();
            var model = new GamesListViewModel { Games = games };
            return this.View(model);
        }

        public IActionResult PreviousGames(int? id)
        {
            if (id != null)
            {
                var teamGames = this.gamesService.GetUpcomingGamesByTeam((int)id);
                var parameterModel = new GamesListViewModel { Games = teamGames };
                return this.View(parameterModel);
            }

            var games = this.gamesService.GetPreviousGames();
            var model = new GamesListViewModel { Games = games };
            return this.View(model);
        }

        public IActionResult UpcomingGamesToday()
        {
            var games = this.gamesService.GetUpcomingGamesToday();
            var model = new GamesListViewModel { Games = games };
            return this.View(model);
        }

        public IActionResult ById(int id)
        {
            var model = this.gamesService.GetById(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }
    }
}
