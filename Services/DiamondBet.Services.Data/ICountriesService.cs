namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Web.ViewModels.Countries;

    public interface ICountriesService
    {
        IEnumerable<CountriesInListViewModel> GetAllCountries();

        CountryByIdViewModel GetById(int id);

        Task AddCountryAsync(AddCountryInputModel inputModel);

        Task EditCountryAsync(EditCountryInputModel inputModel, int id);

        EditCountryInputModel GetCountryDataForEdit(int id);
    }
}
