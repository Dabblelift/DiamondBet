namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;

    public class SelectItemsService : ISelectItemsService
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IRepository<Country> countriesRepository;
        private readonly IDeletableEntityRepository<Stadium> stadiumsRepository;
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;

        public SelectItemsService(
            IDeletableEntityRepository<Team> teamsRepository,
            IRepository<Country> countriesRepository,
            IDeletableEntityRepository<Stadium> stadiumsRepository,
            IDeletableEntityRepository<Competition> competitionsRepository)
        {
            this.teamsRepository = teamsRepository;
            this.countriesRepository = countriesRepository;
            this.stadiumsRepository = stadiumsRepository;
            this.competitionsRepository = competitionsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCompetitionsNames()
        {
            return this.competitionsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCountriesNames()
        {
            return this.countriesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllStadiumsNames()
        {
            return this.stadiumsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllTeamsNames()
        {
            return this.teamsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
