namespace DiamondBet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DiamondBet.Services.Data;
    using DiamondBet.Web.ViewModels.Competitions;
    using Microsoft.AspNetCore.Mvc;

    public class CompetitionsController : BaseController
    {
        private readonly ICompetitionsService competitionsService;
        private readonly ISelectItemsService selectItemsService;

        public CompetitionsController(
            ICompetitionsService competitionsService,
            ISelectItemsService selectItemsService)
        {
            this.competitionsService = competitionsService;
            this.selectItemsService = selectItemsService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.competitionsService.DeleteCompetitionAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All()
        {
            var competitions = this.competitionsService.GetAllCompetitions();
            var model = new CompetitionsListViewModel { Competitions = competitions };
            return this.View(model);
        }

        public IActionResult ById(int id)
        {
            var model = this.competitionsService.GetById(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }
    }
}
