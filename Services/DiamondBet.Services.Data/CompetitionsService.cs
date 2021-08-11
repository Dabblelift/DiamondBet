namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Competitions;
    using DiamondBet.Web.ViewModels.Games;

    public class CompetitionsService : ICompetitionsService
    {
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public CompetitionsService(
            IDeletableEntityRepository<Competition> competitionsRepository,
            IDeletableEntityRepository<Game> gamesRepository)
        {
            this.competitionsRepository = competitionsRepository;
            this.gamesRepository = gamesRepository;
        }

        public async Task AddCompetitionAsync(AddCompetitionInputModel inputModel)
        {
            var competition = new Competition
            {
                Name = inputModel.Name,
                NumberOfParticipants = inputModel.NumberOfParticipants,
                CountryId = inputModel.CountryId,
            };

            await this.competitionsRepository.AddAsync(competition);
            await this.competitionsRepository.SaveChangesAsync();
        }

        public async Task DeleteCompetitionAsync(int id)
        {
            var competition = this.competitionsRepository.All().FirstOrDefault(x => x.Id == id);
            this.competitionsRepository.Delete(competition);
            await this.competitionsRepository.SaveChangesAsync();
        }

        public async Task EditCompetitionAsync(EditCompetitionInputModel inputModel, int id)
        {
            var competition = this.competitionsRepository.All().Where(x => x.Id == id).SingleOrDefault();
            competition.Name = inputModel.Name;
            competition.NumberOfParticipants = inputModel.NumberOfParticipants;
            competition.CountryId = inputModel.CountryId;

            await this.competitionsRepository.SaveChangesAsync();
        }

        public IEnumerable<CompetitionsInListViewModel> GetAllCompetitions()
        {
            return this.competitionsRepository.AllAsNoTracking().Select(x => new CompetitionsInListViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).OrderBy(x => x.Name).ToList();
        }

        public CompetitionByIdViewModel GetById(int id)
        {
            var previousGames = this.gamesRepository
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
                    StartingTime = x.StartingTime.ToLocalTime(),
                }).ToList();

            var upcomingGames = this.gamesRepository
                .AllAsNoTracking()
                .Where(x => x.CompetitionId == id)
                .Where(x => x.StartingTime >= DateTime.UtcNow)
                .OrderBy(x => x.StartingTime)
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
                    StartingTime = x.StartingTime.ToLocalTime(),
                }).ToList();

            return this.competitionsRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new CompetitionByIdViewModel
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Name = x.Name,
                NumberOfParticipants = x.NumberOfParticipants,
                PreviousGames = previousGames,
                UpcomingGames = upcomingGames,
            }).FirstOrDefault();
        }

        public EditCompetitionInputModel GetCompetitionDataForEdit(int id)
        {
            return this.competitionsRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new EditCompetitionInputModel
            {
                Name = x.Name,
                NumberOfParticipants = x.NumberOfParticipants,
                CountryId = x.CountryId,
            }).FirstOrDefault();
        }
    }
}
