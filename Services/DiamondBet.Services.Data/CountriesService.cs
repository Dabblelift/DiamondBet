namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Countries;
    using DiamondBet.Web.ViewModels.Teams;

    public class CountriesService : ICountriesService
    {
        private readonly IRepository<Country> countriesRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public CountriesService(
            IRepository<Country> countriesRepository,
            IDeletableEntityRepository<Team> teamsRepository)
        {
            this.countriesRepository = countriesRepository;
            this.teamsRepository = teamsRepository;
        }

        public async Task AddCountryAsync(AddCountryInputModel inputModel)
        {
            var country = new Country
            {
                Name = inputModel.Name,
                Capital = inputModel.Capital,
            };

            await this.countriesRepository.AddAsync(country);
            await this.countriesRepository.SaveChangesAsync();
        }

        public async Task EditCountryAsync(EditCountryInputModel inputModel, int id)
        {
            var country = this.countriesRepository.All().Where(x => x.Id == id).SingleOrDefault();
            country.Name = inputModel.Name;
            country.Capital = inputModel.Capital;

            await this.countriesRepository.SaveChangesAsync();
        }

        public IEnumerable<CountriesInListViewModel> GetAllCountries()
        {
            return this.countriesRepository.AllAsNoTracking().Select(x => new CountriesInListViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).OrderBy(x => x.Name).ToList();
        }

        public CountryByIdViewModel GetById(int id)
        {
            var teams = this.teamsRepository
                 .AllAsNoTracking()
                 .Where(x => x.CountryId == id)
                 .OrderBy(x => x.Name)
                 .Take(10)
                 .Select(x => new TeamsInCountryViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                 }).ToList();

            return this.countriesRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new CountryByIdViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Capital = x.Capital,
                Teams = teams,
            }).FirstOrDefault();
        }

        public EditCountryInputModel GetCountryDataForEdit(int id)
        {
            return this.countriesRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new EditCountryInputModel
            {
                Name = x.Name,
                Capital = x.Capital,
            }).FirstOrDefault();
        }
    }
}
