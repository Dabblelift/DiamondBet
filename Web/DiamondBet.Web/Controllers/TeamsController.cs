namespace DiamondBet.Web.Controllers
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

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }

        public IActionResult ByCountry(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var teams = this.teamsService.GetTeamsByCountry((int)id);
            var model = new TeamsListViewModel { Teams = teams, };

            return this.View(model);
        }
    }
}
