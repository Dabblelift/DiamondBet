namespace DiamondBet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DiamondBet.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class DeleteController : AdministrationController
    {
        private readonly IGamesService gamesService;
        private readonly ITeamsService teamsService;
        private readonly ICompetitionsService competitionsService;
        private readonly IStadiumsService stadiumsService;

        public DeleteController(
            IGamesService gamesService,
            ITeamsService teamsService,
            ICompetitionsService competitionsService,
            IStadiumsService stadiumsService)
        {
            this.gamesService = gamesService;
            this.teamsService = teamsService;
            this.competitionsService = competitionsService;
            this.stadiumsService = stadiumsService;
        }

        public async Task<IActionResult> Game(int id)
        {
            await this.gamesService.DeleteGameAsync(id);
            return this.Redirect("/Games/UpcomingGames");
        }

        public async Task<IActionResult> Team(int id)
        {
            await this.teamsService.DeleteTeamAsync(id);
            return this.Redirect("/Teams/All");
        }

        public async Task<IActionResult> Competition(int id)
        {
            await this.competitionsService.DeleteCompetitionAsync(id);
            return this.Redirect("/Competitions/All");
        }

        public async Task<IActionResult> Stadium(int id)
        {
            await this.stadiumsService.DeleteStadiumAsync(id);
            return this.Redirect("/Stadiums/All");
        }
    }
}
