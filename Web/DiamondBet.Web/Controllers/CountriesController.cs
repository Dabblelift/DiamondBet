namespace DiamondBet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DiamondBet.Services.Data;
    using DiamondBet.Web.ViewModels.Countries;
    using Microsoft.AspNetCore.Mvc;

    public class CountriesController : BaseController
    {
        private readonly ICountriesService countriesService;

        public CountriesController(
            ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult All()
        {
            var countries = this.countriesService.GetAllCountries();
            var model = new CountriesListViewModel { Countries = countries };
            return this.View(model);
        }

        public IActionResult ById(int id)
        {
            var model = this.countriesService.GetById(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.countriesService.GetCountryDataForEdit(id);

            if (model == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCountryInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.countriesService.EditCountryAsync(input, id);

            return this.RedirectToAction(nameof(this.ById), new { id = id });
        }
    }
}
