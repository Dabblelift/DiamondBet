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

    public class EditController : AdministrationController
    {
        private readonly IGamesService gamesService;
        private readonly ITeamsService teamsService;
        private readonly ICompetitionsService competitionsService;
        private readonly IStadiumsService stadiumsService;
        private readonly ICountriesService countriesService;
        private readonly IBetsService betsService;
        private readonly ISelectItemsService selectItemsService;

        public EditController(
            IGamesService gamesService,
            ITeamsService teamsService,
            ICompetitionsService competitionsService,
            IStadiumsService stadiumsService,
            ICountriesService countriesService,
            IBetsService betsService,
            ISelectItemsService selectItemsService)
        {
            this.gamesService = gamesService;
            this.teamsService = teamsService;
            this.competitionsService = competitionsService;
            this.stadiumsService = stadiumsService;
            this.countriesService = countriesService;
            this.betsService = betsService;
            this.selectItemsService = selectItemsService;
        }

        [HttpGet]
        public IActionResult Game(int id)
        {
            var model = this.gamesService.GetGameDataForEdit(id);

            if (model == null)
            {
                return this.Redirect("/Error");
            }

            model.TeamsItems = this.selectItemsService.GetAllTeamsNames();
            model.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
            model.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Game(EditGameInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.TeamsItems = this.selectItemsService.GetAllTeamsNames();
                input.CompetitionsItems = this.selectItemsService.GetAllCompetitionsNames();
                input.StadiumsItems = this.selectItemsService.GetAllStadiumsNames();
                return this.View(input);
            }

            await this.gamesService.EditGameAsync(input, id);

            if (input.HomeGoals != null && input.AwayGoals != null)
            {
                await this.betsService.SettleBetsOnGameResultEditAsync(input, id);
            }

            return this.Redirect("/Games/ById/" + id.ToString());
        }

        [HttpGet]
        public IActionResult Team(int id)
        {
            var model = this.teamsService.GetTeamDataForEdit(id);

            if (model == null)
            {
                return this.Redirect("/Error");
            }

            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Team(EditTeamInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.teamsService.EditTeamAsync(input, id);

            return this.Redirect("/Teams/ById/" + id.ToString());
        }

        [HttpGet]
        public IActionResult Competition(int id)
        {
            var model = this.competitionsService.GetCompetitionDataForEdit(id);

            if (model == null)
            {
                return this.Redirect("/Error");
            }

            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Competition(EditCompetitionInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.competitionsService.EditCompetitionAsync(input, id);

            return this.Redirect("/Competitions/ById/" + id.ToString());
        }

        [HttpGet]
        public IActionResult Stadium(int id)
        {
            var model = this.stadiumsService.GetStadiumDataForEdit(id);

            if (model == null)
            {
                return this.Redirect("/Error");
            }

            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Stadium(EditStadiumInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.stadiumsService.EditStadiumAsync(input, id);

            return this.Redirect("/Stadiums/ById/" + id.ToString());
        }

        [HttpGet]
        public IActionResult Country(int id)
        {
            var model = this.countriesService.GetCountryDataForEdit(id);

            if (model == null)
            {
                return this.Redirect("/Error");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Country(EditCountryInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.countriesService.EditCountryAsync(input, id);

            return this.Redirect("/Countries/ById/" + id.ToString());
        }
    }
}
