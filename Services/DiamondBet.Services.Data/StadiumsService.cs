namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Games;
    using DiamondBet.Web.ViewModels.Stadiums;

    public class StadiumsService : IStadiumsService
    {
        private readonly IDeletableEntityRepository<Stadium> stadiumsRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public StadiumsService(
            IDeletableEntityRepository<Stadium> stadiumsRepository,
            IDeletableEntityRepository<Game> gamesRepository)
        {
            this.stadiumsRepository = stadiumsRepository;
            this.gamesRepository = gamesRepository;
        }

        public async Task AddStadiumAsync(AddStadiumInputModel inputModel)
        {
            var stadium = new Stadium
            {
                Name = inputModel.Name,
                Capacity = inputModel.Capacity,
                YearFounded = inputModel.YearFounded,
                CountryId = inputModel.CountryId,
            };

            await this.stadiumsRepository.AddAsync(stadium);
            await this.stadiumsRepository.SaveChangesAsync();
        }

        public async Task DeleteStadiumAsync(int id)
        {
            var stadium = this.stadiumsRepository.All().FirstOrDefault(x => x.Id == id);
            this.stadiumsRepository.Delete(stadium);
            await this.stadiumsRepository.SaveChangesAsync();
        }

        public async Task EditStadiumAsync(EditStadiumInputModel inputModel, int id)
        {
            var stadium = this.stadiumsRepository.All().Where(x => x.Id == id).SingleOrDefault();
            stadium.Name = inputModel.Name;
            stadium.Capacity = inputModel.Capacity;
            stadium.YearFounded = inputModel.YearFounded;
            stadium.CountryId = inputModel.CountryId;

            await this.stadiumsRepository.SaveChangesAsync();
        }

        public IEnumerable<StadiumsInListViewModel> GetAllStadiums()
        {
            return this.stadiumsRepository.AllAsNoTracking().Select(x => new StadiumsInListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Capacity = x.Capacity,
            }).OrderBy(x => x.Name).ToList();
        }

        public StadiumByIdViewModel GetById(int id)
        {
            var games = this.gamesRepository
                .AllAsNoTracking()
                .Where(x => x.CompetitionId == id)
                .Where(x => x.StartingTime < DateTime.UtcNow)
                .OrderByDescending(x => x.StartingTime)
                .Take(10)
                .Select(x => new FilteredGamesViewModel
                {
                    GameId = x.Id,
                    AwayGoals = x.AwayGoals,
                    AwayTeamId = x.AwayTeamId,
                    AwayTeamName = x.AwayTeam.Name,
                    HomeGoals = x.HomeGoals,
                    HomeTeamId = x.HomeTeamId,
                    HomeTeamName = x.HomeTeam.Name,
                    StartingTime = x.StartingTime.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                }).ToList();

            return this.stadiumsRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new StadiumByIdViewModel
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Name = x.Name,
                Capacity = x.Capacity,
                YearFounded = x.YearFounded,
                Games = games,
            }).FirstOrDefault();
        }

        public EditStadiumInputModel GetStadiumDataForEdit(int id)
        {
            return this.stadiumsRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new EditStadiumInputModel
            {
                Name = x.Name,
                Capacity = x.Capacity,
                YearFounded = x.YearFounded,
                CountryId = x.CountryId,
            }).FirstOrDefault();
        }
    }
}
