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
        private readonly ISelectItemsService selectItemsService;

        public GamesController(
            IGamesService gamesService,
            ISelectItemsService selectItemsService)
        {
            this.gamesService = gamesService;
            this.selectItemsService = selectItemsService;
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            var model = new AddGameInputModel();
            model.TeamsItems = this.selectItemsService.GetAllTeamsNames();
            model.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
            model.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddGame(AddGameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.TeamsItems = this.selectItemsService.GetAllTeamsNames();
                input.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
                input.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();
                return this.View(input);
            }

            await this.gamesService.AddGameAsync(input);

            return this.RedirectToAction(nameof(this.UpcomingGames));
        }

        [HttpGet]
        public IActionResult EditGame(int id)
        {
            var model = this.gamesService.GetGameIdForEdit(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            model.TeamsItems = this.selectItemsService.GetAllTeamsNames();
            model.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
            model.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditGame(EditGameInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.TeamsItems = this.selectItemsService.GetAllTeamsNames();
                input.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
                input.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();
                return this.View(input);
            }

            await this.gamesService.EditGameAsync(input, id);

            return this.RedirectToAction(nameof(this.ById), new { id = id });
        }

        public async Task<IActionResult> DeleteGame(int id)
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
