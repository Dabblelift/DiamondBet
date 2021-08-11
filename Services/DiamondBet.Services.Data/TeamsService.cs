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
    using DiamondBet.Web.ViewModels.Teams;

    public class TeamsService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public TeamsService(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Game> gamesRepository)
        {
            this.teamsRepository = teamsRepository;
            this.gamesRepository = gamesRepository;
        }

        public async Task AddTeamAsync(AddTeamInputModel inputModel)
        {
            var team = new Team
            {
                Name = inputModel.Name,
                Nickname = inputModel.Nickname,
                CountryId = inputModel.CountryId,
                YearFounded = inputModel.YearFounded,
            };

            await this.teamsRepository.AddAsync(team);
            await this.teamsRepository.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = this.teamsRepository.All().FirstOrDefault(g => g.Id == id);
            this.teamsRepository.Delete(team);
            await this.teamsRepository.SaveChangesAsync();
        }

        public async Task EditTeamAsync(EditTeamInputModel inputModel, int id)
        {
            var team = this.teamsRepository.All().Where(x => x.Id == id).SingleOrDefault();
            team.Name = inputModel.Name;
            team.Nickname = inputModel.Nickname;
            team.CountryId = inputModel.CountryId;
            team.YearFounded = inputModel.YearFounded;

            await this.teamsRepository.SaveChangesAsync();
        }

        public IEnumerable<TeamsInListViewModel> GetAllTeams()
        {
            return this.teamsRepository.AllAsNoTracking().Select(x => new TeamsInListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
            }).OrderBy(x => x.Name).ToList();
        }

        public TeamByIdViewModel GetById(int id)
        {
            var previousGames = this.gamesRepository
                .AllAsNoTracking()
                .Where(x => x.HomeTeamId == id || x.AwayTeamId == id)
                .Where(x => x.StartingTime < DateTime.UtcNow)
                .OrderByDescending(x => x.StartingTime)
                .Take(5)
                .Select(x => new TeamGamesViewModel
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
                .Where(x => x.HomeTeamId == id || x.AwayTeamId == id)
                .Where(x => x.StartingTime >= DateTime.UtcNow)
                .OrderBy(x => x.StartingTime)
                .Take(5)
                .Select(x => new TeamGamesViewModel
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

            return this.teamsRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new TeamByIdViewModel
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Name = x.Name,
                NickName = x.Nickname,
                YearFounded = x.YearFounded,
                PreviousGames = previousGames,
                UpcomingGames = upcomingGames,
            }).FirstOrDefault();
        }

        public EditTeamInputModel GetTeamDataForEdit(int id)
        {
            return this.teamsRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new EditTeamInputModel
            {
                Name = x.Name,
                Nickname = x.Nickname,
                CountryId = x.CountryId,
                YearFounded = x.YearFounded,
            }).FirstOrDefault();
        }
    }
}
