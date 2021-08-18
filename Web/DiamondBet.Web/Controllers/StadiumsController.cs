namespace DiamondBet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DiamondBet.Services.Data;
    using DiamondBet.Web.ViewModels.Stadiums;
    using Microsoft.AspNetCore.Mvc;

    public class StadiumsController : BaseController
    {
        private readonly IStadiumsService stadiumsService;
        private readonly ISelectItemsService selectItemsService;

        public StadiumsController(
            IStadiumsService stadiumsService,
            ISelectItemsService selectItemsService)
        {
            this.stadiumsService = stadiumsService;
            this.selectItemsService = selectItemsService;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.stadiumsService.GetStadiumDataForEdit(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            model.CountriesItems = this.selectItemsService.GetAllCountriesNames();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditStadiumInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.selectItemsService.GetAllCountriesNames();
                return this.View(input);
            }

            await this.stadiumsService.EditStadiumAsync(input, id);

            return this.RedirectToAction(nameof(this.ById), new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.stadiumsService.DeleteStadiumAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All()
        {
            var stadiums = this.stadiumsService.GetAllStadiums();
            var model = new StadiumsListViewModel { Stadiums = stadiums };
            return this.View(model);
        }

        public IActionResult ById(int id)
        {
            var model = this.stadiumsService.GetById(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }
    }
}
