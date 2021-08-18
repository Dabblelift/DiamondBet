namespace DiamondBet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DiamondBet.Services.Data;
    using DiamondBet.Web.ViewModels.Competitions;
    using DiamondBet.Web.ViewModels.Countries;
    using DiamondBet.Web.ViewModels.Games;
    using DiamondBet.Web.ViewModels.Stadiums;
    using DiamondBet.Web.ViewModels.Teams;
    using Microsoft.AspNetCore.Mvc;

    public class AddController : AdministrationController
    {
        private readonly IGamesService gamesService;
        private readonly ITeamsService teamsService;
        private readonly ICompetitionsService competitionsService;
        private readonly IStadiumsService stadiumsService;
        private readonly ICountriesService countriesService;
        private readonly ISelectItemsService selectItemsService;

        public AddController(
            IGamesService gamesService,
            ITeamsService teamsService,
            ICompetitionsService competitionsService,
            IStadiumsService stadiumsService,
            ICountriesService countriesService,
            ISelectItemsService selectItemsService)
        {
            this.gamesService = gamesService;
            this.teamsService = teamsService;
            this.competitionsService = competitionsService;
            this.stadiumsService = stadiumsService;
            this.countriesService = countriesService;
            this.selectItemsService = selectItemsService;
        }

        [HttpGet]
        public IActionResult Game()
        {
            var model = new AddGameInputModel();
            model.TeamsItems = this.selectItemsService.GetAllTeamsNames();
            model.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
            model.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Game(AddGameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.TeamsItems = this.selectItemsService.GetAllTeamsNames();
                input.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
                input.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();
                return this.View(input);
            }

            await this.gamesService.AddGameAsync(input);
            this.TempData["Message"] = "A new game was added successfully";

            return this.Redirect("/Games/UpcomingGames");
        }

        [HttpGet]
        public IActionResult Team()
        {
            var model = new AddTeamInputModel();
            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Team(AddTeamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.teamsService.AddTeamAsync(input);
            this.TempData["Message"] = "A new team was added successfully.";

            return this.Redirect("/Teams/All");
        }

        [HttpGet]
        public IActionResult Competition()
        {
            var model = new AddCompetitionInputModel();
            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Competition(AddCompetitionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.competitionsService.AddCompetitionAsync(input);
            this.TempData["Message"] = "A new competition was added successfully.";

            return this.Redirect("/Competitions/All");
        }

        [HttpGet]
        public IActionResult Stadium()
        {
            var model = new AddStadiumInputModel();
            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Stadium(AddStadiumInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.stadiumsService.AddStadiumAsync(input);
            this.TempData["Message"] = "A new stadium was added successfully.";

            return this.Redirect("/Stadiums/All");
        }

        [HttpGet]
        public IActionResult Country()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Country(AddCountryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.countriesService.AddCountryAsync(input);
            this.TempData["Message"] = "A new country was added successfully";

            return this.Redirect("/Countries/All");
        }
    }
}
