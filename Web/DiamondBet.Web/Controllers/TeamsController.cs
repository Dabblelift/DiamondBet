﻿namespace DiamondBet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DiamondBet.Services.Data;
    using DiamondBet.Web.ViewModels.Teams;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : BaseController
    {
        private readonly ITeamsService teamsService;
        private readonly ISelectItemsService selectItemsService;

        public TeamsController(
            ITeamsService teamsService,
            ISelectItemsService selectItemsService)
        {
            this.teamsService = teamsService;
            this.selectItemsService = selectItemsService;
        }

        public IActionResult All()
        {
            var teams = this.teamsService.GetAllTeams();
            var model = new TeamsListViewModel { Teams = teams };
            return this.View(model);
        }

        public IActionResult ById(int id)
        {
            var model = this.teamsService.GetById(id);
            return this.View(model);
        }

        public IActionResult ByCountry(string country)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddTeam()
        {
            var model = new AddTeamInputModel();
            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(AddTeamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.teamsService.AddTeamAsync(input);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpGet]
        public IActionResult EditTeam(int id)
        {
            var model = this.teamsService.GetTeamIdForEdit(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTeam(EditTeamInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.teamsService.EditTeamAsync(input, id);

            return this.RedirectToAction(nameof(this.ById), new { id = id });
        }

        public async Task<IActionResult> DeleteTeam(int id)
        {
            await this.teamsService.DeleteTeamAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}